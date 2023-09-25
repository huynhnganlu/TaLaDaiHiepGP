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
    public int maxHealth = 100, maxShield = 100;
    public int currentHealth;
    private int currentShield;
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
    public event DataKillHandle onKillEnemy;
    #endregion
    #region Skill variables
    public List<SkillAbstract> skillList;
    public delegate void SkillHandle(List<SkillAbstract> skillList);
    public event SkillHandle SkillListChange;
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
        #region Animation ref
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        #endregion

        #region Character Data ref
        currentHealth = maxHealth;
        currentShield = maxShield;
        healthBar.maxValue = maxHealth;
        shieldBar.maxValue = maxShield;
        healthBar.value = maxHealth;
        shieldBar.value = maxShield;
        healthText.text = currentHealth.ToString();
        shieldText.text = currentShield.ToString();

        expBar.maxValue = maxExp;
        #endregion
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
        onKillEnemy += HandleKillEnemy;
        SkillListChange += HandleSkillListChange;
    }
    //Object disable va inactive thi xoa listener
    private void OnDisable()
    {
        onKillEnemy -= HandleKillEnemy;
        SkillListChange += HandleSkillListChange;
    }
    #region General
    //Nhan damage tu quai vat
    public void TakeEnemyDamage(int damage)
    {
        if(currentShield > 0)
        {
            currentShield -= damage;
            shieldBar.value = currentShield;
            shieldText.text = currentShield.ToString();

        }
        else
        {
            if(currentHealth - damage > 0)
            {
                currentHealth -= damage;
                healthBar.value = currentHealth;
                healthText.text = currentHealth.ToString();
            }
            else
            {
                currentHealth -= damage;
                healthBar.value = currentHealth;
                healthText.text = "0";
                MapController.Instance.ProcessFinishMap();
            }
           
        }
      
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
    #endregion
    #region Kill Enemy Observer
    //Thong bao data khi giet duoc 1 quai vat bat ky
    public void AddKillEnemyChange(int exp, int money, int qi)
    {
        onKillEnemy?.Invoke(exp, money, qi);
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
    #region Skill Observer
    //Thong bao data khi thay doi skill
    public void AddSkillListChange(List<SkillAbstract> skillList)
    {
        SkillListChange?.Invoke(skillList);
    }
    //Them listener xy ly thay doi skill
    public void HandleSkillListChange(List<SkillAbstract> skillList)
    {
        foreach (SkillAbstract skill in skillList)
        {
            skill.InvokeSkill();
        }
    }
    #endregion
   
}
