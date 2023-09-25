using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapUIController : MonoBehaviour
{
    public ScrollRect scrollRect;
    private float scrollSpeed = 25f;
    public delegate void MapSelectEvent(bool status, string orientation);
    public event MapSelectEvent MapSelect;
    public static MapUIController Instance { get; private set; }
    public string currentMap = "Map01";

    public Button buttonFight;
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

    private void Start()
    {
        buttonFight.onClick.AddListener(() =>
        {
            ButtonStartFight();
        });
    }

    private void OnEnable()
    {
        MapSelect += HandleButtonScroll;
    }

    private void OnDisable()
    {
        MapSelect -= HandleButtonScroll;
    }

    private void HandleButtonScroll(bool status, string orientation)
    {
        if (status)
        {
            if (orientation.Equals("left"))
            {
                if (scrollRect.horizontalNormalizedPosition >= 0f)
                    scrollRect.horizontalNormalizedPosition -= scrollSpeed * Time.deltaTime;
            }
            else if (orientation.Equals("right"))
            {
                if (scrollRect.horizontalNormalizedPosition <= 1f)
                    scrollRect.horizontalNormalizedPosition += scrollSpeed * Time.deltaTime;

            }
        }
    }

    public void ScrollButtonClick(bool status,string orientation)
    {
        MapSelect?.Invoke(status, orientation);
    }

    private void ButtonStartFight()
    {
        SceneManager.LoadScene("Map01");
    }
}
