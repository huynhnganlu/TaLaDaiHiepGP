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
    private int maxHealth, maxShield = 100;
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

    //Skill variables
    public GameObject skill;

    //Observer handle variables
    public delegate void DataKillHandle(int exp, int money, int qi);
    public event DataKillHandle OnKillEnemy;
    

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

        InvokeRepeating("CallSkills", 2f, 2f);
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

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        OnKillEnemy += HandleKillEnemy;
    }


    private void OnDisable()
    {
        OnKillEnemy -= HandleKillEnemy;
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
    //Instance skill ra
    void CallSkills()
    {
        GameObject arrow = Instantiate(skill, transform.position + new Vector3(2f,0f,0f), Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(3.0f,0f);
    }
    //Them function tiep nhan su thay doi cho event
    public void AddKillEnemyChange(int exp, int money, int qi)
    {
        OnKillEnemy?.Invoke(exp, money, qi);
    }
    //Them observer cho su thay doi cua cac gia tri khi giet duoc quai vat
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
    //Note: Su dung operator = cho PlayerData no chi reference den file, neu file thay doi trang thai co the viec doc se khong duoc xay ra, do do chung ta can set tung gia tri
    public void LoadData()
    {
        PlayerData dataLoad = SaveSystem.LoadPlayerData();
        currentLevel = dataLoad.level;
    }

   

}
