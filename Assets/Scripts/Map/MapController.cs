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

    //Timer variables
    public Slider timeSlider;
    private float time = 600f;
    public TextMeshProUGUI timeText;
    private float minute = 0f;
    private float second = 0f;

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
        foreach(Sprite sprite in characterData.innerImage)
        {
            if(sprite != null)
            {
                GameObject item = Instantiate(innerMapItem);
                item.GetComponent<Image>().sprite = sprite;
                item.transform.SetParent(parentInnerHolder.transform);
            }
        }
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
    //Xu ly logic khi nguoi choi chon phan thuong
    private void OnPlayerSelectPrize()
    {
        TogglePrize(false);
        SetFreezing(false);
    }
}
