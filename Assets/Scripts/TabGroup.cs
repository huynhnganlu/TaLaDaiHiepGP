using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabItem> tabItems;
    public Sprite tabIdle,tabActive;
    public List<GameObject> swapContent;

    public void addTabItems(TabItem tabItem)
    {
        if(tabItems == null)
        {
            tabItems = new List<TabItem>();
        }

        tabItems.Add(tabItem);
    }

    public void OnTabSelected(TabItem tabItem)
    {
        ResetTabs();
        tabItem.background.sprite = tabActive;
        int index = tabItem.transform.GetSiblingIndex();
        for(int i = 0; i < swapContent.Count; i++)
        {
            if(i == index)
            {
                swapContent[i].SetActive(true);
            }
            else
            {
                swapContent[i].SetActive(false);
            }
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
