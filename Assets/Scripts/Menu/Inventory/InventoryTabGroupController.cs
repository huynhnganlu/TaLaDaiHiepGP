using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTabGroupController : TabGroupAbstract
{
    public ScrollRect scrollRect;
    private TabItem selectedTabItem;
    public TabItem defaultTabItem;
    // Start is called before the first frame update
    void Start()
    {
        selectedTabItem = defaultTabItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTabSelected(TabItem tabItem)
    {
        ResetTabs();
        int index = tabItem.transform.GetSiblingIndex();
        for (int i = 0; i < swapContent.Count; i++)
        {
            if (i == index)
            {
                swapContent[i].SetActive(true);
                scrollRect.content = swapContent[i].GetComponent<RectTransform>();
            }
            else
            {
                swapContent[i].SetActive(false);
            }
        }
        selectedTabItem = tabItem;
    }
}
