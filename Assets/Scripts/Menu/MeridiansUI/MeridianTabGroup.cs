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
        //Xu ly meridian - Reset va get property data - set level
        if (selectedTabItem != tabItem)
        {
            selectedTabItem = tabItem;
            ResetTabs();
            tabItem.background.sprite = tabActive;
            MeridianController.Instance.imageHolder.sprite = tabItem.GetComponent<MeridianAbstract>().merdianImage;
            tabItem.GetComponent<MeridianAbstract>().GetPropertyData();
        }
    }
}
