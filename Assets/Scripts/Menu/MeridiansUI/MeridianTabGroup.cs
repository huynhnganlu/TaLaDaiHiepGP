using UnityEngine;

public class MeridianTabGroup : TabGroupAbstract
{
    public TabItem selectedTabItem;
    public TabItem defaultTabItem;

    private void OnEnable()
    {
        if(selectedTabItem == null)
            selectedTabItem = defaultTabItem;
        selectedTabItem.transform.localScale = new Vector3(1.1f, 1f, 1f);
        selectedTabItem.GetComponent<MeridianAbstract>().GetPropertyData();
    }

    public override void OnTabSelected(TabItem tabItem)
    {
        //Xu ly meridian - Reset va get property data - set level
        if (selectedTabItem != tabItem)
        {
            selectedTabItem = tabItem;
            ResetTabs();
            tabItem.background.sprite = tabActive;
            MeridianController.Instance.imageHolder.sprite = tabItem.GetComponent<MeridianAbstract>().merdianImage;
            selectedTabItem.transform.localScale = new Vector3(1.1f, 1f, 1f);
            tabItem.GetComponent<MeridianAbstract>().GetPropertyData();
        }
    }
}
