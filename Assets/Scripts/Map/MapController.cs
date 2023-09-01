using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [HideInInspector]
    public bool isFreezing = false;

    //Singleton
    public static MapController Instance { get; private set; }

    public GameObject finishMapUI;

    public GameObject prizeObject;
    public Button processFinishMapButton;
    public Button[] processPrizeButtons;

    [SerializeField]
    private TextMeshProUGUI money, qi;
    [SerializeField]
    private CharacterData characterData;

    [SerializeField]
    private GameObject parentInnerHolder, innerMapItem;


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
        processFinishMapButton.onClick.AddListener(() =>
        {
            SetFreezing(false);
            characterData.money = MyCharacterController.Instance.money;
            characterData.qi = MyCharacterController.Instance.qi;
            LoadSceneMainMenu();
        });
        foreach(Button btn in processPrizeButtons)
        {
            btn.onClick.AddListener(() =>
            {
                OnPlayerSelectPrize();
            });
        }
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
