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

    public override void OnTabSelected(TabItem tabItem)
    {
        if(tabItem != selectedTabItem)
        {
            ResetTabs();         
            selectedTabItem = tabItem;
        }
    }
}
