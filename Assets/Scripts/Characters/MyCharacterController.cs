using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyCharacterController : MonoBehaviour
{
    //Animation vs Physic variables
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private float speed = 7.0f;

    //Health vs Shiled variables
    private int maxHealth = 100, maxShield = 100;
    public int currentHealth;
    private int currentShield;
    public Slider healthBar, shieldBar;
    public TextMeshProUGUI healthText, shieldText;

    //Level variables
    public int currentLevel, maxExp, currentExp;
    [SerializeField]
    private Slider expBar;

    //Money variables
    public int money = 0;

    //Meridians value variables
    public int qi = 0;
  
    //Singleton variables
    public static MyCharacterController Instance { get; private set; }

    //Observer handle variables
    public delegate void DataKillHandle(int exp, int money, int qi);
    public event DataKillHandle onKillEnemy;

    //Skill variables
    public List<SkillAbstract> skillList;
    public delegate void SkillHandle(List<SkillAbstract> skillList);
    public event SkillHandle skillListChange;

    //Singleton
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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

  
    // Update is called once per frame
    void Update()
    {
        if(MapController.Instance.isFreezing == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
      
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
        skillListChange += HandleSkillListChange;
    }
    //Object disable va inactive thi xoa listener
    private void OnDisable()
    {
        onKillEnemy -= HandleKillEnemy;
        skillListChange += HandleSkillListChange;
    }
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
    //Function tiep nhan su thay doi cho event (Khi giet duoc 1 quai vat bat ky)
    public void AddKillEnemyChange(int exp, int money, int qi)
    {
        onKillEnemy?.Invoke(exp, money, qi);
    }
    //Them listener xy ly cac thay doi khi giet duoc quai vat
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
    public void SkillListChange(List<SkillAbstract> skillList)
    {
        skillListChange?.Invoke(skillList);
    }
    public void HandleSkillListChange(List<SkillAbstract> skillList)
    {
        foreach (SkillAbstract skill in skillList)
        {
            skill.InvokeSkill();
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
    //Trigger save
    public void SaveData()
    {
        SaveSystem.SavePlayerData(this);
    }
    //Trigger load
    //Note: Su dung operator = cho PlayerData no chi reference den file, neu file thay doi trang thai co the viec doc se khong duoc xay ra, do do can set tung gia tri
    public void LoadData()
    {
        PlayerData dataLoad = SaveSystem.LoadPlayerData();
        currentLevel = dataLoad.level;
    }
    
   

}
