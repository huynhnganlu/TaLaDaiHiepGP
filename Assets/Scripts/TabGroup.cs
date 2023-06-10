using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabItem> tabItems;
    public Sprite tabIdle,tabActive;
    public List<GameObject> swapContent;
    private TabItem selectedTabItem;
    private GameObject disableTab;
    public ScrollRect parentScrollRect;
    

    public void addTabItems(TabItem tabItem)
    {
        if(tabItems == null)
        {
            tabItems = new List<TabItem>();
        }

        tabItems.Add(tabItem);
    }

    void Start()
    {
        selectedTabItem = tabItems[0];
        disableTab = GameObject.Find("SubCollectionsTab");
    }

    public void OnTabSelected(TabItem tabItem)
    {
        if(selectedTabItem != tabItem)
        {
            if (tabItem.tag.Equals("CollectionsTab"))
            {
                if (tabItem == tabItems[0])
                {
                    disableTab.SetActive(true);
                }
                else
                {
                    disableTab.SetActive(false);
                }
            }
            ResetTabs();
            tabItem.background.sprite = tabActive;
            int index = tabItem.transform.GetSiblingIndex();
            for (int i = 0; i < swapContent.Count; i++)
            {
                if (i == index)
                {
                    swapContent[i].SetActive(true);           
                    parentScrollRect.content = swapContent[i].GetComponent<RectTransform>();
                }
                else
                {
                    swapContent[i].SetActive(false);
                }
            }
            selectedTabItem = tabItem;
        }
    }

    public void ResetTabs()
    {
        foreach(TabItem tabItem in tabItems)
        {
            tabItem.background.sprite = tabIdle;
        }
        
    }
    
}
