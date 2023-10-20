using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyCharacterController : MonoBehaviour
{
    #region Animation vs Physic variables
    private Rigidbody2D rb;
    public Vector2 movement;
    private Animator animator;
    public float speed = 7.0f;
    private bool isFliped = false;
    private SpriteRenderer spriteRenderer;
    #endregion
    #region Health vs Shiled variables
    [HideInInspector]
    public int maxHealth, maxShield, currentHealth, currentShield;
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
    #endregion
    #region Pref variables
    private JsonPlayerPrefs characterPrefs, shopPrefs;
    #endregion
    #region Property variables
    [HideInInspector]
    public int externalDamage, internalDamage, critDamage, defense, hpRegen, mpRegen, elementalYin, elementalYang, elementalTaichi;
    [HideInInspector]
    public float movementSpeed, evade, critRate;
    [HideInInspector]
    public bool isImmune, isEvade = false;
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
        moneyText = GameObject.Find("Canvas/SkillMoneyHolder/SkillMoneyBG/SkillMoneyValue").GetComponent<TextMeshProUGUI>();
        GetProperty();
        HandleInner("Buff");
        SetProperty();

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
        StartCoroutine(RegenOverTime());
    }


    void Update()
    {
        #region Movement Process
        if (MapController.Instance.isFreezing == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (movement.x == 1)
            {
                if (isFliped == true)
                {
                    spriteRenderer.transform.localScale = new Vector3(-spriteRenderer.transform.localScale.x, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
                }
                animator.SetBool("Moving", true);
                isFliped = false;
            }
            else if (movement.x == -1)
            {
                if (isFliped == false)
                {
                    spriteRenderer.transform.localScale = new Vector3(-spriteRenderer.transform.localScale.x, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
                }
                animator.SetBool("Moving", true);
                isFliped = true;
            }
            else if (movement.y == 1 || movement.y == -1)
            {
                animator.SetBool("Moving", true);
            }
            else if (movement.x == 0 && movement.y == 0)
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
        isEvade = EvadeProcess();
        if (!isEvade)
        {
            HandleInner("Defense");
            if (!isImmune)
            {
                if (damage - defense >= 0)
                    damage -= defense;
                else
                    damage = 0;
                if (currentShield - damage >= 0)
                {
                    SetShield(currentShield - damage);
                }
                else
                {
                    if (currentShield > 0)
                    {
                        SetHealth(currentHealth - damage - currentShield);
                        SetShield(0);
                    }
                    else
                    {
                        if (currentHealth - damage > 0)
                        {
                            SetHealth(currentHealth - damage);
                        }
                        else
                        {
                            SetHealth(0);
                            StartCoroutine(MapController.Instance.ProcessFinishMap());
                        }
                    }
                }
                HandleInner("Health");
            }
        }
        isEvade = false;
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
        internalDamage = characterPrefs.GetInt("internalDamage");
        critRate = characterPrefs.GetInt("critRate");
        critDamage = characterPrefs.GetInt("critDamage");
        defense = characterPrefs.GetInt("defense");
        hpRegen = characterPrefs.GetInt("hpRegen");
        mpRegen = characterPrefs.GetInt("mpRegen");
        movementSpeed = characterPrefs.GetInt("movementSpeed");
        speed += movementSpeed;
        GetInnerElemental();
    }
    private void GetInnerElemental()
    {
        if (shopPrefs.HasKey("slot0"))
        {
            for(int i = 0; i <= 2; i++)
            {
                int innerID = shopPrefs.GetInt("slot" + i);
                if(innerID != -1)
                {
                    string elemental = MapController.Instance.innerHolder.listInner[innerID].GetComponent<ShopDataAbstract>().itemElemental;
                    switch (elemental)
                    {
                        case "yang":
                            elementalYang += 1;
                            break;
                        case "yin":
                            elementalYin += 1;
                            break;
                        case "taichi":
                            elementalTaichi += 1;
                            break;
                    }
                }
            }
        }

    }
    private void SetProperty()
    {
        healthBar.maxValue = maxHealth;
        shieldBar.maxValue = maxShield;
        SetHealth(maxHealth);
        SetShield(maxShield);
        expBar.maxValue = maxExp;
    }
    public void SetHealth(int hp)
    {
        currentHealth = hp;
        healthBar.value = hp;
        healthText.text = hp.ToString();
    }
    public void SetShield(int mp)
    {
        currentShield = mp;
        shieldBar.value = mp;
        shieldText.text = mp.ToString();
    }
    IEnumerator RegenOverTime()
    {
        while (currentHealth > 0)
        {
            if (currentHealth < maxHealth)
            {
                if (currentHealth + hpRegen >= maxHealth)
                    currentHealth = maxHealth;
                else
                    currentHealth += hpRegen;
                SetHealth(currentHealth);
            }
            if (currentShield < maxShield)
            {
                if (currentShield + mpRegen >= maxShield)
                    currentShield = maxShield;
                else
                    currentShield += mpRegen;
                SetShield(currentShield);
            }
            yield return new WaitForSeconds(10);
        }
    }
    #endregion
    #region Kill Enemy    
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
        for (int i = 0; i < skillList.Count; i++)
        {
            if (skillList[i] != null)
            {
                MapController.Instance.skillMapItems[i].sprite = skillList[i].icon;
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
    public bool EvadeProcess()
    {
        float randomValue = Random.value;
        float evadeRate = evade / 100f;
        if (randomValue <= evadeRate)
            return true;
        return false;
    }
    #endregion   
    #region Inner
    public void HandleInner(string type)
    {
        List<ShopDataAbstract> list = MapController.Instance.equipedList;
        if (list != null && list.Count != 0)
        {
            foreach (ShopDataAbstract inner in list)
            {
                if (inner.itemType.Equals(type))
                {
                    inner.ItemEffect();
                }
            }
        }
    }

    #endregion


}
