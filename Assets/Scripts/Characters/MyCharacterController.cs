using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyCharacterController : MonoBehaviour
{
    #region Animation vs Physic variables
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    public float speed = 7.0f;
    #endregion
    #region Health vs Shiled variables
    [HideInInspector]
    public int maxHealth = 100, maxShield = 100, currentHealth, currentShield;
    public Slider healthBar, shieldBar;
    public TextMeshProUGUI healthText, shieldText;
    #endregion
    #region Level variables
    public int currentLevel, maxExp, currentExp;
    [SerializeField]
    private Slider expBar;
    #endregion
    #region Money variables
    public int money = 0;
    #endregion
    #region Meridians value variables
    public int qi = 0;
    #endregion
    #region Singleton variables
    public static MyCharacterController Instance { get; private set; }
    #endregion
    #region Data kill variables
    public delegate void DataKillHandle(int exp, int money, int qi);
    public event DataKillHandle OnKillEnemy;
    #endregion
    #region Skill variables
    public List<SkillAbstract> skillList;
    #endregion
    #region Pref variables
    private JsonPlayerPrefs characterPrefs, shopPrefs;
    #endregion
    #region Property variables
    [HideInInspector]
    public int evade, externalDamage, externalCrit, skipExternalDefense, externalDefense, internalDamage, internalCrit, skipInternalDefense, internalDefense, mpRegen;
    #endregion
    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion
    }
    void Start()
    {
        #region Pref ref
        characterPrefs = MapController.Instance.characterPrefs;
        shopPrefs = MapController.Instance.shopPrefs;
        #endregion
        #region Animation ref
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        #endregion
        #region Character
        GetProperty();
        SetProperty();
        #endregion
        HandleSkill(skillList);
    }


    void Update()
    {
        #region Movement Process
        if (MapController.Instance.isFreezing == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        #endregion
    }
    //Xu ly di chuyen
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    //Object enable va active thi them listener
    private void OnEnable()
    {
        OnKillEnemy += HandleKillEnemy;
    }
    //Object disable va inactive thi xoa listener
    private void OnDisable()
    {
        OnKillEnemy -= HandleKillEnemy;
    }
    #region Character
    //Nhan damage tu quai vat
    public void TakeEnemyDamage(int damage)
    {
        if(currentShield - damage >= 0)
        {
            currentShield -= damage;
            shieldBar.value = currentShield;
            shieldText.text = currentShield.ToString();

        }
        else
        {
            if(currentShield > 0)
            {
                currentHealth -= (damage - currentShield);
                currentShield = 0;
                healthBar.value = currentHealth;
                shieldBar.value = currentShield;
                healthText.text = currentHealth.ToString();
                shieldText.text = currentShield.ToString();
            }
            else
            {
                if (currentHealth - damage > 0)
                {
                    currentHealth -= damage;
                    healthBar.value = currentHealth;
                    healthText.text = currentHealth.ToString();
                }
                else
                {
                    currentHealth = 0;
                    healthBar.value = currentHealth;
                    healthText.text = "0";              
                    MapController.Instance.ProcessFinishMap();
                }
            }
          
           
        }
        HandleInner("Health");
    }
    //Function len cap
    private void LevelUp()
    {
        currentExp = 0;
        currentLevel++;
        maxExp += 100;
        expBar.maxValue = maxExp;
        MapController.Instance.TogglePrize(true);
    }
    private void GetProperty()
    {
        maxHealth = 100 + characterPrefs.GetInt("hp");
        maxShield = 100 + characterPrefs.GetInt("mp");
        evade = characterPrefs.GetInt("evade");
        externalDamage = characterPrefs.GetInt("externalDamage");
        externalCrit = characterPrefs.GetInt("externalCrit");
        externalDefense = characterPrefs.GetInt("externalDefense");
        skipExternalDefense = characterPrefs.GetInt("skipExternalDefense");
        internalDamage = characterPrefs.GetInt("internalDamage");
        internalCrit = characterPrefs.GetInt("internalCrit");
        internalDefense = characterPrefs.GetInt("internalDefense");
        skipInternalDefense = characterPrefs.GetInt("skipInternalDefense");
        mpRegen = characterPrefs.GetInt("mpRegen");

    }
    private void SetProperty()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        healthBar.maxValue = maxHealth;
        shieldBar.maxValue = maxShield;
        healthBar.value = maxHealth;
        shieldBar.value = maxShield;
        healthText.text = currentHealth.ToString();
        shieldText.text = currentShield.ToString();

        expBar.maxValue = maxExp;
    }
    public void SetHealth(int hp)
    {
        currentHealth = hp;
        healthBar.value = hp;
        healthText.text = hp.ToString();
    }
    #endregion
    #region Kill Enemy Observer
    //Thong bao data khi giet duoc 1 quai vat bat ky
    public void AddKillEnemyChange(int exp, int money, int qi)
    {
        OnKillEnemy?.Invoke(exp, money, qi);
    }
    //Them listener xy ly khi giet duoc quai vat
    private void HandleKillEnemy(int exp, int _money, int _qi)
    {
        currentExp += exp;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
        expBar.value = currentExp;
        money += _money;
        qi += _qi;
    }
    #endregion
    #region Skill Handle
    public void HandleSkill(List<SkillAbstract> skillList)
    {
        foreach (SkillAbstract skill in skillList)
        {
            skill.InvokeSkill();
        }
    }
    #endregion
    #region Damage Handle

    public void HandleInner(string type)
    {
        List<ShopDataAbstract> list = MapController.Instance.equipedList;
        if(list != null && list.Count != 0)
        {
            foreach(ShopDataAbstract inner in list)
            {
                if (inner.itemType.Equals(type))
                {
                    inner.ItemEffect();
                }
            }
        }
    }
    #endregion
  
    #region Inner

    #endregion

}
