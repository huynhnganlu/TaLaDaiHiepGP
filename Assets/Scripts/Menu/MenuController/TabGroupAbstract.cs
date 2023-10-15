using System.Collections.Generic;
using UnityEngine;

public abstract class TabGroupAbstract : MonoBehaviour
{
    private List<TabItem> tabItems;
    public Sprite tabIdle, tabActive;
    public List<GameObject> swapContent;
    //Them cac item cua group de xu ly
    public void AddTabItems(TabItem tabItem)
    {
        tabItems ??= new List<TabItem>();

        tabItems.Add(tabItem);
    }
    //Reset sprite cua cac item thanh idle
    public void ResetTabs()
    {
        foreach (TabItem tabItem in tabItems)
        {
            tabItem.background.sprite = tabIdle;
        }

    }
    //Click vao item cua group
    public abstract void OnTabSelected(TabItem tabItem);
}
