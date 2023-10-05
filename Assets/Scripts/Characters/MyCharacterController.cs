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
    private bool isFliped = false;
    private SpriteRenderer spriteRenderer;
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
    public int money;
    public TextMeshProUGUI moneyText;
    #endregion
    #region Meridians value variables
    public int qi = 0;
    #endregion
    #region Singleton variables
    public static MyCharacterController Instance { get; private set; }
    #endregion

    #region Skill variables
    public List<PrizeAbstract> skillList;
    [SerializeField]
    private PrizeAbstract defaultSkill;
    [SerializeField]
    private Image[] skillMapItems;
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
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        #endregion
        #region Character
        healthBar = GameObject.Find("Canvas/HPBar").GetComponent<Slider>();
        shieldBar = GameObject.Find("Canvas/ShieldBar").GetComponent<Slider>();
        healthText = GameObject.Find("Canvas/HPBar/Image/HPText").GetComponent<TextMeshProUGUI>();
        shieldText = GameObject.Find("Canvas/ShieldBar/Image/ShieldText").GetComponent<TextMeshProUGUI>();
        expBar = GameObject.Find("Canvas/ExpBarSlider").GetComponent<Slider>();
        /*  GetProperty();
          SetProperty(); */
        #endregion
        skillList = new List<PrizeAbstract>(4)
        {
            defaultSkill,
            null,
            null,
            null,
        };
        HandleSkill(skillList);
        money = 0;
        moneyText.text = money.ToString();
    }


    void Update()
    {
        #region Movement Process
        if (MapController.Instance.isFreezing == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if(movement.x == 1)
            {
                if (isFliped == true)
                {
                    spriteRenderer.transform.localScale = new Vector3(-spriteRenderer.transform.localScale.x, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
                }
                animator.SetBool("Moving", true);
                isFliped = false;
            }else if(movement.x == -1)
            {
                if (isFliped == false)
                {
                    spriteRenderer.transform.localScale = new Vector3(-spriteRenderer.transform.localScale.x, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
                }
                animator.SetBool("Moving", true);
                isFliped = true;
            }else if(movement.y == 1 || movement.y == -1)
            {
                animator.SetBool("Moving", true);
            }
            else if(movement.x == 0 && movement.y == 0)
            {
                animator.SetBool("Moving", false);
            }
        }
        #endregion
    }
    //Xu ly di chuyen
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
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
        MapController.Instance.TogglePrize("buff");
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
    #region Kill Enemy    
    //Them listener xy ly khi giet duoc quai vat
    public void HandleKillEnemy(int exp, int _money, int _qi)
    {
        currentExp += exp;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
        expBar.value = currentExp;
        money += _money;
        moneyText.text = money.ToString();
        qi += _qi;
    }
    #endregion
    #region Skill Handle
    public void HandleSkill(List<PrizeAbstract> skillList)
    {
        for (int i = 0;i < skillList.Count;i++)
        {
            if(skillList[i] != null)
            {
                skillMapItems[i].sprite = skillList[i].icon;
                skillList[i].skillRef.InvokeSkill();
            }
        }
    }
    public void ResetSkill()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i] != null)
            {
                skillList[i].skillRef.CancelSkill();
            }
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
