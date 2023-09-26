using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    #region FreezingMap var
    //Freezing map variable
    [HideInInspector]
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
    private float totalRate;
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
    private float time = 600f;
    public TextMeshProUGUI timeText;
    private float minute = 0f;
    private float second = 0f;
    #endregion
    #region Skill var
    public List<SkillAbstract> skillList;
    public SkillAbstract testSkill;
    #endregion
    #region SpawnEnemy var
    [SerializeField]
    private LayerMask layerNotSpawn;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private Collider2D colliderSpawnEnemies;
    #endregion
    #region Pref var
    public JsonPlayerPrefs shopPrefs;
    public JsonPlayerPrefs characterPrefs;
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

        #region Pref ref
        characterPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/CharacterData.json");
        shopPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/ShopData.json");
        #endregion
    }

    private void Start()
    {
        OnSkillListChange(testSkill);

        //spawn enemy
        StartCoroutine(SpawnEnemies(colliderSpawnEnemies, enemies));

        //Khoi tao list skill
        skillList ??= new List<SkillAbstract>();

        //Set gia tri mac dinh cho timer
        if (time > 60f)
        {
            minute = time / 60f;
            second = time % 60f;
        }

        //Reference player
        player = MyCharacterController.Instance.gameObject;

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
        //Lay total rate cua cac prize
        totalRate = GetTotalRate();
    }

    private void Update()
    {
        #region timer -1/s + set value
        if (second <= 0f)
        {
            minute -= 1f;
            second = 59f;
        }
        second -= 1 * Time.deltaTime;
        timeText.text = minute.ToString("0") + ":" + second.ToString("0");
        time -= 1 * Time.deltaTime;
        timeSlider.value = time;
        #endregion
    }

    private void LateUpdate()
    {
        #region transform + boundary minimap
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, minimapCamera.transform.position.z);
        targetPos.x = Mathf.Clamp(targetPos.x,minimapMinPos.x,minimapMaxPos.x);
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
    //Xu ly logic khi nguoi choi giet duoc boss
    public void ProcessFinishMap()
    {
        finishMapUI.SetActive(true);
        SetFreezing(true);
        money.text = MyCharacterController.Instance.money.ToString();
        qi.text = MyCharacterController.Instance.qi.ToString();
    }
    //Load menu chinh
    private void LoadSceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
    #region Prize
    //Xu ly logic khi nguoi choi len level
    public void TogglePrize(bool status)
    {
        prizeObject.SetActive(status);
        SetFreezing(true);
        GetPrize();
    }
    public void GetPrize()
    {
        for (int i = 0; i <= 2; i++)
        {
            SetPrizeData(RandomPrize(), i);
        }
    }
    //Set gia tri cua prize vao prizeUI
    private void SetPrizeData(PrizeAbstract prize, int slot)
    {
        PrizeUI prizeItem = prizeItems[slot].GetComponent<PrizeUI>();
        prizeItem.id = prize.id;
        prizeItem.header.text = prize.header;
        prizeItem.icon.sprite = prize.icon;
        prizeItem.description.text = prize.description;

    }
    //Lay random prize
    private PrizeAbstract RandomPrize()
    {
        float value = Random.value * totalRate;
        float sumRate = 0;
        foreach (PrizeAbstract prize in prizeHolder.prizeList)
        {
            sumRate += prize.rate;
            if (sumRate >= value)
                return prize;
        }
        return default;
    }
    //Lay total rate tu prizeList
    private float GetTotalRate()
    {
        float totalRate = 0f;
        foreach (PrizeAbstract prize in prizeHolder.prizeList)
        {
            totalRate += prize.rate;
        }
        return totalRate;
    }
    //Xu ly logic khi nguoi choi chon phan thuong
    public void OnPrizeItemClick(int slot)
    {
        prizeHolder.prizeList[prizeItems[slot].GetComponent<PrizeUI>().id].GetComponent<PrizeAbstract>().ProcessPrize();
        TogglePrize(false);
        SetFreezing(false);

    }
    #endregion
    #region SpawnEnemy
    //Ham spawn enemy
    IEnumerator SpawnEnemies(Collider2D collider, GameObject[] enemies)
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            foreach (GameObject enemy in enemies)
            {
                Instantiate(enemy, GetRandomSpawnPosition(collider), Quaternion.identity);
            }
        }
    }
    //Ham kiem tra xem vi tri random co phu hop khong
    private Vector2 GetRandomSpawnPosition(Collider2D collider)
    {
        Vector2 randomPos = Vector2.zero;
        bool isValidPos = false;
        while(!isValidPos)
        {
            randomPos = GetRandomPointInCollider(collider);
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

            if (!isInvalidCollision && (Vector2.Distance(MyCharacterController.Instance.transform.position, randomPos) > 13f))
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
        if (prefs.HasKey("slot1"))
        {
            equipedList ??= new List<ShopDataAbstract>();
            equipedList.Clear();
            for(int i = 0;i <= 2; i++)
            {
                if(prefs.GetInt("slot"+i) == -1)
                {
                    innerMapItems[i].sprite = null;
                    innerMapItems[i].color = new Color(1f, 1f, 1f, 0f);
                }
                else
                {
                    ShopDataAbstract data = innerHolder.listInner[prefs.GetInt("slot" + i)].GetComponent<ShopDataAbstract>();
                    GameObject clonePrefab = Instantiate(data.gameObject);
                    equipedList.Add(clonePrefab.GetComponent<ShopDataAbstract>());
                    innerMapItems[i].sprite = data.itemImage;
                    innerMapItems[i].color = new Color(1f, 1f, 1f, 1f);
                }
            }
            MyCharacterController.Instance.HandleInner("Buff");
        }
    }
    #endregion
    #region Skill
    //Xu ly khi skill list thay doi
    public void OnSkillListChange(SkillAbstract skill)
    {
        skillList.Add(skill);
        MyCharacterController.Instance.AddSkillListChange(skillList);
    }
    #endregion
}
