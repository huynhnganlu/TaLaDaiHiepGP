using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class MeridianTabGroup : TabGroupAbstract
{
    public TabItem selectedTabItem;
    public TabItem defaultTabItem;

    private void Start()
    {
        selectedTabItem = defaultTabItem;
    }

    public override void OnTabSelected(TabItem tabItem)
    {
        if (selectedTabItem != tabItem)
        {
            selectedTabItem = tabItem;
            ResetTabs();
            tabItem.background.sprite = tabActive;
            MeridianController.Instance.SetMeridianLevel(tabItem.GetComponent<MeridianAbstract>().level);
        }
    }
}
