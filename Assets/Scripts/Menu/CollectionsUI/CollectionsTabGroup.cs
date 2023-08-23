using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionsTabGroup : TabGroupAbstract
{
    private GameObject disableTab;
    public ScrollRect parentScrollRect;
    private TabItem selectedTabItem;
    public TabItem defaultTabItem;

    void Start()
    {   
        selectedTabItem = defaultTabItem;
        disableTab = GameObject.Find("SubCollectionsTab");
    }

    public override void OnTabSelected(TabItem tabItem)
    {
        if(selectedTabItem != tabItem)
        {
            //Tab dac biet danh cho 4 sub collections tab
            if (tabItem.tag.Equals("CollectionsTab"))
            {
                if (tabItem == defaultTabItem)
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
  
}
