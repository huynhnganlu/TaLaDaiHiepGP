using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class TabGroupAbstract : MonoBehaviour
{
    private List<TabItem> tabItems;
    public Sprite tabIdle, tabActive;
    public List<GameObject> swapContent;

    public void addTabItems(TabItem tabItem)
    {
        if (tabItems == null)
        {
            tabItems = new List<TabItem>();
        }

        tabItems.Add(tabItem);
    }

    public void ResetTabs()
    {
        foreach (TabItem tabItem in tabItems)
        {
            tabItem.background.sprite = tabIdle;
        }

    }

    public abstract void OnTabSelected(TabItem tabItem); 
}
