using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    //Freezing map variable
    [HideInInspector]
    public bool isFreezing = false;

    //Singleton
    public static MapController Instance { get; private set; }


    //Prize variables
    public GameObject prizeObject;
    public GameObject[] prizeItems;

    //Finish map variables
    public GameObject finishMapUI;
    public Button processFinishMapButton;
    
    //Minimap variables
    public Camera minimapCamera;
    private GameObject player;
    public Vector2 minimapMaxPos;
    public Vector2 minimapMinPos;

    //Character asset variables
    [SerializeField]
    private TextMeshProUGUI money, qi;
    [SerializeField]
    private CharacterData characterData;

    //Inner variables
    [SerializeField]
    private GameObject parentInnerHolder, innerMapItem;

    //Time variables
    public Slider timeSlider;
    private float time = 600f;
    public TextMeshProUGUI timeText;
    private float minute = 0f;
    private float second = 0f;

    //Skill variables
    public List<SkillAbstract> skillList;
    public SkillAbstract testSkill;

    //Spawn random enemy variables
    [SerializeField]
    private LayerMask layerNotSpawn;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private Collider2D colliderSpawnEnemies;

    //Inner variables
    [SerializeField]
    private InnerHolder innerHolder;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
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

        //Tim gameObject player
        player = GameObject.FindGameObjectWithTag("Player");

        //Gan event cho finish map button
        processFinishMapButton.onClick.AddListener(() =>
        {
            SetFreezing(false);
            characterData.money = MyCharacterController.Instance.money;
            characterData.qi = MyCharacterController.Instance.qi;
            LoadSceneMainMenu();
        });

       

        //Show cac noi cong da trang bi
     /*   foreach(Sprite sprite in characterData.innerImage)
        {
            if(sprite != null)
            {
                GameObject item = Instantiate(innerMapItem);
                item.GetComponent<Image>().sprite = sprite;
                item.transform.SetParent(parentInnerHolder.transform);
            }
        }*/
    }

    private void Update()
    {
        //Xu ly gia tri cua timer -1 moi giay va set value cua timer slider
        if (second <= 0f)
        {
            minute -= 1f;
            second = 59f;
        }
        second -= 1 * Time.deltaTime;
        timeText.text = minute.ToString("0") + ":" + second.ToString("0");
        time -= 1 * Time.deltaTime;
        timeSlider.value = time;

    }

    //Set transform + boundary cho minimap camera
    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, minimapCamera.transform.position.z);
        targetPos.x = Mathf.Clamp(targetPos.x,minimapMinPos.x,minimapMaxPos.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minimapMinPos.y, minimapMaxPos.y);
        minimapCamera.transform.position = Vector3.Lerp(minimapCamera.transform.position, targetPos, 0.5f);
    }

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
    //Xu ly logic khi nguoi choi len level
    public void TogglePrize(bool status)
    {
        prizeObject.SetActive(status);
        SetFreezing(true);
    }
    //Xu ly khi skill list thay doi
    private void OnSkillListChange(SkillAbstract skill)
    {
        skillList.Add(skill);
        MyCharacterController.Instance.SkillListChange(skillList);
    }
    //Xu ly logic khi nguoi choi chon phan thuong
    public void OnPrizeItemClick()
    {
        OnSkillListChange(testSkill);
        TogglePrize(false);
        SetFreezing(false);
    }
    //Ham spawn enemy
    IEnumerator SpawnEnemies(Collider2D collider, GameObject[] enemies)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

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

}
