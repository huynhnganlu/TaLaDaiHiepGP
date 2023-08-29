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

    public static MapController Instance { get; private set; }

    public GameObject finishMapUI;

  
    public GameObject prizeObject;
    public Button processFinishMapButton;
    public Button[] processPrizeButtons;
    [SerializeField]
    private TextMeshProUGUI money, qi;
    [SerializeField]
    private CharacterData characterData;



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
       
    }
    public void SetFreezing(bool status)
    {
        isFreezing = status;
        if (isFreezing)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

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

    public void TogglePrize(bool status)
    {
        prizeObject.SetActive(status);
        SetFreezing(true);
    }
    private void OnPlayerSelectPrize()
    {
        TogglePrize(false);
        SetFreezing(false);
    }
}
