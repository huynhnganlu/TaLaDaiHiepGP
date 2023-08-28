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
            loadSceneMainMenu();
        });
        foreach(Button btn in processPrizeButtons)
        {
            btn.onClick.AddListener(() =>
            {
                OnPlayerSelectPrize();
            });
        }
       
    }
    public void setFreezing(bool status)
    {
        isFreezing = status;
        if (isFreezing)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void processFinishMap()
    {
        finishMapUI.SetActive(true);
        setFreezing(true);
        money.text = MyCharacterController.Instance.money.ToString();
        qi.text = MyCharacterController.Instance.qi.ToString();
    }

    private void loadSceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void togglePrize(bool status)
    {
        prizeObject.SetActive(status);
        setFreezing(true);
    }
    private void OnPlayerSelectPrize()
    {
        togglePrize(false);
        setFreezing(false);
    }
}
