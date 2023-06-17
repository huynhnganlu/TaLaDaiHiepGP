using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeridianTabGroup : TabGroupAbstract
{
    private TabItem selectedTabItem;
    public TabItem defaultTabItem;

    private void Start()
    {
        if (defaultTabItem != null)
            selectedTabItem = defaultTabItem;
    }

    public override void OnTabSelected(TabItem tabItem)
    {
        if (selectedTabItem != tabItem)
        {
            ResetTabs();
            tabItem.background.sprite = tabActive;
            int index = tabItem.transform.GetSiblingIndex();
            for (int i = 0; i < swapContent.Count; i++)
            {
                if (i == index)
                {
                    swapContent[i].SetActive(true);
                }
                else
                {
                    swapContent[i].SetActive(false);
                }
            }
            selectedTabItem = tabItem;
        }
    }
}
