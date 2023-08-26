using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public ScrollRect scrollRect;
    private float scrollSpeed = 0.3f;
    public delegate void MapSelectEvent(bool status, string orientation);
    public event MapSelectEvent mapSelect;
    public static MapController Instance { get; private set; }
    public string currentMap = "Map01";
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
        
    }

    private void OnEnable()
    {
        mapSelect += HandleButtonScroll;
    }

    private void OnDisable()
    {
        mapSelect -= HandleButtonScroll;
    }

    private void HandleButtonScroll(bool status, string orientation)
    {
        if (orientation.Equals("left"))
        {
            if(scrollRect.horizontalNormalizedPosition >= 0f)
                scrollRect.horizontalNormalizedPosition -= scrollSpeed * Time.deltaTime;
        }else if (orientation.Equals("right"))
        {
            if (scrollRect.horizontalNormalizedPosition <= 1f)
                scrollRect.horizontalNormalizedPosition += scrollSpeed;
            
        }
    }

    public void ScrollButtonClick(bool status,string orientation)
    {
        mapSelect?.Invoke(status, orientation);
    }
}
