using System.Collections.Generic;

public class CharacterTabGroup : TabGroupAbstract
{

    public static TabItem selectedTabItem;
    public TabItem defaultTabItem;
    private Dictionary<string, int>[] characterSpecial;
    private Dictionary<string, int> swordSpecial;
    private Dictionary<string, int> bladeSpecial;
    private Dictionary<string, int> fistSpecial;


    public override void OnTabSelected(TabItem tabItem)
    {
        if (selectedTabItem != tabItem)
        {
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
            CharacterUIController.Instance.ResetCharacterPoints();
            CharacterUIController.Instance.UpdateCharacterPoints(characterSpecial[tabItem.transform.GetSiblingIndex()]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        swordSpecial ??= new Dictionary<string, int>()
            {
                {"movementSpeed", 1}
            };
        bladeSpecial ??= new Dictionary<string, int>()
            {
                {"critDamage", 2}
            };
        fistSpecial ??= new Dictionary<string, int>()
            {
                {"hp", 50}
            };
        characterSpecial ??= new Dictionary<string, int>[]
        {
            swordSpecial,
            bladeSpecial,
            fistSpecial,
        };
        selectedTabItem = defaultTabItem;
        CharacterUIController.Instance.UpdateCharacterPoints(swordSpecial);
    }

}
