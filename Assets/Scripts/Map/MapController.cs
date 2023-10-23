using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    #region FreezingMap var
    //Freezing map variable
    public bool isFreezing = false;
    #endregion
    #region Singleton var
    //Singleton
    public static MapController Instance { get; private set; }
    #endregion
    #region Prize var
    public GameObject prizeObject;
    public GameObject[] prizeItems;
    public PrizeHolder prizeHolder;
    private string prizeType;
    #endregion
    #region FinishMap var
    //Finish map variables
    public GameObject finishMapUI;
    public Button processFinishMapButton;
    [SerializeField]
    private TextMeshProUGUI money, qi;
    #endregion
    #region Minimap var
    public Camera minimapCamera;
    private GameObject player;
    public Vector2 minimapMaxPos;
    public Vector2 minimapMinPos;
    #endregion
    #region Inner var
    [SerializeField]
    private Image[] innerMapItems;
    public InnerHolder innerHolder;
    public List<ShopDataAbstract> equipedList;
    #endregion
    #region Time var
    public Slider timeSlider;
    private float time = 300f, minute = 0f, second = 0f;
    public TextMeshProUGUI timeText;
    #endregion
    #region Skill var
    public Image[] skillMapItems;
    #endregion
    #region SpawnEnemy var
    [SerializeField]
    private LayerMask layerNotSpawn;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private Collider2D colliderSpawnEnemies;
    private float timeSpawn = 5f;
    private Coroutine spawnCoroutine;
    #endregion
    #region Pref var
    public JsonPlayerPrefs shopPrefs;
    public JsonPlayerPrefs characterPrefs;
    #endregion
    public GameObject damageText, vcamera, boss;
    private int keySkill;
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

        #region Pref ref
        characterPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/CharacterData.json");
        shopPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/ShopData.json");
        #endregion
    }

    private void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnEnemies(enemies, timeSpawn));

        //Set gia tri mac dinh cho timer    
        StartCoroutine(TimerProcess());

        //Reference player
        //player = MyCharacterController.Instance.gameObject;

        //Gan event cho finish map button
        processFinishMapButton.onClick.AddListener(() =>
        {
            SetFreezing(false);
            //characterData.money = MyCharacterController.Instance.money;
            //characterData.qi = MyCharacterController.Instance.qi;
            LoadSceneMainMenu();
        });

        //Hien thi cac noi cong da trang bi
        GetEquipedInner(shopPrefs);

        //Camera follow player
        vcamera.GetComponent<ICinemachineCamera>().Follow = MyCharacterController.Instance.transform;
    }

    private void Update()
    {
        #region skill prize
        if (isFreezing == false)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4)) && MyCharacterController.Instance.money >= 50)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    keySkill = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    keySkill = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    keySkill = 2;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    keySkill = 3;
                }
                MyCharacterController.Instance.SetMoney(50);
                TogglePrize("skill");
            }
        }

        #endregion
    }

    private void LateUpdate()
    {
        #region transform + boundary minimap
        Vector3 targetPos = new Vector3(MyCharacterController.Instance.transform.position.x, MyCharacterController.Instance.transform.position.y, minimapCamera.transform.position.z);
        targetPos.x = Mathf.Clamp(targetPos.x, minimapMinPos.x, minimapMaxPos.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minimapMinPos.y, minimapMaxPos.y);
        minimapCamera.transform.position = Vector3.Lerp(minimapCamera.transform.position, targetPos, 0.5f);
        #endregion
    }

    #region General
    //Freezing gameplay khi toggle UI
    public void SetFreezing(bool status)
    {
        isFreezing = status;
        if (isFreezing)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    //Xu ly logic khi nguoi choi giet duoc boss hoac chet
    public IEnumerator ProcessFinishMap()
    {
        MyCharacterController.Instance.isImmune = true;
        isFreezing = true;
        MyCharacterController.Instance.movement.x = 0;
        MyCharacterController.Instance.movement.y = 0;
        yield return new WaitForSeconds(1.5f);
        SetFreezing(true);
        finishMapUI.SetActive(true);
        money.text = MyCharacterController.Instance.money.ToString();
        qi.text = MyCharacterController.Instance.qi.ToString();
    }
    //Load menu chinh
    private void LoadSceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator TimerProcess()
    {
        if (time >= 0f)
        {
            minute = Mathf.Floor(time / 60f);
            second = time % 60f;
        }
        timeSlider.maxValue = time;
        while (time >= 0)
        {
            if (time == 0)
            {
                timeSlider.value = boss.GetComponent<BossController>().currentEnemyHP;
                timeText.text = boss.GetComponent<BossController>().currentEnemyHP.ToString();
                BossProcess();
                yield break;
            }
            if (second == 0 && minute != 0)
            {
                minute -= 1;
                StopCoroutine(spawnCoroutine);
                timeSpawn -= 0.5f;
                spawnCoroutine = StartCoroutine(SpawnEnemies(enemies, timeSpawn));
                second = 59;
            }
            else if (second != 0)
            {
                second -= 1;
            }
            time -= 1;
            timeSlider.value = time;
            timeText.text = minute.ToString("0") + ":" + second.ToString("0");
            yield return new WaitForSeconds(1);
        }

    }
    private void BossProcess()
    {
        Instantiate(boss, GetRandomSpawnPosition(10f, 13f), Quaternion.identity);
        timeSlider.maxValue = boss.GetComponent<BossController>().enemyMaxHP;
        timeSlider.value = timeSlider.maxValue;
        timeText.text = boss.GetComponent<BossController>().enemyMaxHP.ToString();
    }
    #endregion
    #region Prize
    //Xu ly logic khi nguoi choi len level
    public void TogglePrize(string type)
    {
        prizeObject.SetActive(true);
        SetFreezing(true);
        prizeType = type;      
        GetPrize();       
    }

    public void GetPrize()
    {
        for (int i = 0; i <= 2; i++)
        {
            SetPrizeData(RandomPrize(prizeType), i);
        }
    }
    public void RerollPrize()
    {
        if(MyCharacterController.Instance.money >= 50)
        {
            GetPrize();
            MyCharacterController.Instance.SetMoney(50);
        }
    }
    //Set gia tri cua prize vao prizeUI
    private void SetPrizeData(PrizeAbstract prize, int slot)
    {
        PrizeUI prizeItem = prizeItems[slot].GetComponent<PrizeUI>();
        prizeItem.id = prize.id;
        if (prizeType.Equals("skill"))
        {
            prizeItem.costObject.SetActive(true);
            int totalCost = prize.cost;        
            if (MyCharacterController.Instance.skillDictionary[keySkill] == prize.id)
            {
                if (MyCharacterController.Instance.levelDictionary[keySkill] < 4)
                    prizeItem.header.text = prize.header + "\n<size=80%>Lv." + (MyCharacterController.Instance.levelDictionary[keySkill] + 1);
                else
                    prizeItem.header.text = prize.header + "\n<size=80%>Lv.Max";
                prizeItem.description.text = prize.description + " +" + (prize.skillRef.skillDamage * MyCharacterController.Instance.levelDictionary[keySkill]) + " > +" + (int)System.Math.Round(prize.skillRef.skillDamage * (MyCharacterController.Instance.levelDictionary[keySkill] + 0.5))  ;
                totalCost = prize.cost * MyCharacterController.Instance.levelDictionary[keySkill] * 3;
            }
            else
            {
                prizeItem.header.text = prize.header + "\n<size=80%>Lv.1";
                prizeItem.description.text = prize.description + " +" + (prize.skillRef.skillDamage * MyCharacterController.Instance.levelDictionary[keySkill]);
            }
            prizeItem.costText.text = totalCost.ToString();
            prizeItem.cost = totalCost;
        }
        else
        {
            prizeItem.header.text = prize.header;
            prizeItem.description.text = prize.description;
            prizeItem.costObject.SetActive(false);

        }
        prizeItem.icon.sprite = prize.icon;
    }
    //Lay random prize
    private PrizeAbstract RandomPrize(string type)
    {
        float value = Random.value * GetTotalRate(type);
        float sumRate = 0;
        if (type.Equals("buff"))
        {
            foreach (PrizeAbstract prize in prizeHolder.prizeBuffList)
            {
                sumRate += prize.rate;
                if (sumRate >= value)
                    return prize;
            }
        }
        else if (type.Equals("skill"))
        {
            foreach (PrizeAbstract prize in prizeHolder.prizeSkillList)
            {
                if (MyCharacterController.Instance.levelDictionary[keySkill] == 5 && MyCharacterController.Instance.skillDictionary[keySkill] == prize.id || CheckPrizeMatch(prize.id))
                {
                    value -= prize.rate;        
                    if(prize.id == prizeHolder.prizeSkillList.Count - 1)
                    {
                        foreach(PrizeAbstract rollPrizeAgain in prizeHolder.prizeSkillList)
                        {
                            if (!CheckPrizeMatch(rollPrizeAgain.id))
                            {
                                return rollPrizeAgain;
                            }
                        }
                    }
                }
                else
                {
                    sumRate += prize.rate;
                    if (sumRate >= value)
                        return prize;
                }        
            }
        }
        return default;
    }
    private Boolean CheckPrizeMatch(int id)
    {
        for (int i = 0; i < 4; i++)
        {
            if (keySkill != i && MyCharacterController.Instance.skillDictionary[i] == id)
            {
                return true;
            }
        }
        return false;
    }
    //Lay total rate tu prizeList
    private float GetTotalRate(string type)
    {
        float totalRate = 0f;
        if (type.Equals("buff"))
        {
            foreach (PrizeAbstract prize in prizeHolder.prizeBuffList)
            {
                totalRate += prize.rate;
            }
        }
        else if (type.Equals("skill"))
        {
            foreach (PrizeAbstract prize in prizeHolder.prizeSkillList)
            {
                totalRate += prize.rate;
            }
        }
        return totalRate;
    }
    //Xu ly logic khi nguoi choi chon phan thuong
    public void OnPrizeItemClick(int slot)
    {

        if (prizeType.Equals("buff"))
        {
            prizeHolder.prizeBuffList[prizeItems[slot].GetComponent<PrizeUI>().id].GetComponent<PrizeAbstract>().ProcessPrize();
            ClosePrize();
        }
        else if (prizeType.Equals("skill"))
        {
            PrizeUI prizeClicked = prizeItems[slot].GetComponent<PrizeUI>();
            int totalPrizeCost;
            if (MyCharacterController.Instance.skillDictionary[keySkill] == prizeClicked.id)
                totalPrizeCost = 60 * MyCharacterController.Instance.levelDictionary[keySkill] * 3;
            else
                totalPrizeCost = 60;
            if (MyCharacterController.Instance.money - totalPrizeCost >= 0)
            {
                MyCharacterController.Instance.SetMoney(totalPrizeCost);
                MyCharacterController.Instance.HandleSkill(keySkill, prizeClicked.id);
                ClosePrize();
            }
        }
    }

    public void ClosePrize()
    {
        prizeObject.SetActive(false);
        SetFreezing(false);
    }
    #endregion
    #region SpawnEnemy
    //Ham spawn enemy
    IEnumerator SpawnEnemies(GameObject[] enemies, float timeSpawn)
    {
        while (true)
        {
            foreach (GameObject enemy in enemies)
            {
                ObjectPoolController.Instance.SpawnObject(enemy, GetRandomSpawnPosition(13f, 25f), Quaternion.identity);
            }

            yield return new WaitForSeconds(timeSpawn);

        }
    }
    //Ham kiem tra xem vi tri random co phu hop khong
    public Vector2 GetRandomSpawnPosition(float distanceMin, float distanceMax)
    {
        Vector2 randomPos = Vector2.zero;
        bool isValidPos = false;
        while (!isValidPos)
        {
            randomPos = GetRandomPointInCollider(colliderSpawnEnemies);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPos, 1.5f);
            bool isInvalidCollision = false;

            foreach (Collider2D _collider in colliders)
            {
                if (((1 << _collider.gameObject.layer) & layerNotSpawn) != 0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if (!isInvalidCollision && (Vector2.Distance(MyCharacterController.Instance.transform.position, randomPos) > distanceMin)
                && (Vector2.Distance(MyCharacterController.Instance.transform.position, randomPos) < distanceMax))
            {
                isValidPos = true;
            }
        }
        return randomPos;

    }
    //Ham lay vi tri random trong collider
    private Vector2 GetRandomPointInCollider(Collider2D collider)
    {
        return new Vector2(Random.Range(collider.bounds.min.x, collider.bounds.max.x), Random.Range(collider.bounds.min.y, collider.bounds.max.y));
    }
    #endregion
    #region Inner
    private void GetEquipedInner(JsonPlayerPrefs prefs)
    {
        if (prefs.HasKey("slot0"))
        {
            equipedList ??= new List<ShopDataAbstract>();
            equipedList.Clear();
            for (int i = 0; i <= 2; i++)
            {
                if (prefs.GetInt("slot" + i) == -1)
                {
                    innerMapItems[i].sprite = null;
                    innerMapItems[i].color = new UnityEngine.Color(1f, 1f, 1f, 0f);
                }
                else
                {
                    ShopDataAbstract data = innerHolder.listInner[prefs.GetInt("slot" + i)].GetComponent<ShopDataAbstract>();
                    GameObject clonePrefab = Instantiate(data.gameObject);
                    equipedList.Add(clonePrefab.GetComponent<ShopDataAbstract>());
                    innerMapItems[i].sprite = data.itemImage;
                    innerMapItems[i].color = new UnityEngine.Color(1f, 1f, 1f, 1f);
                }
            }
            MyCharacterController.Instance.HandleInner("Buff");
        }
    }
    #endregion

}
