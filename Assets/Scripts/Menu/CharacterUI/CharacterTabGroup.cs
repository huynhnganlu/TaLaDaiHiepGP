using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
            ResetTabs();
            selectedTabItem = tabItem;
            selectedTabItem.transform.localScale = new Vector3(1.1f, 1f, 1f);
            switch (tabItem.name)
            {
                case "Sword":
                    MenuController.Instance.characterPrefs.SetString("character", "Sword");
                    break;
                case "Blade":
                    MenuController.Instance.characterPrefs.SetString("character", "Blade");
                    break;
                case "Fist":
                    MenuController.Instance.characterPrefs.SetString("character", "Fist");
                    break;
            }
            MenuController.Instance.characterPrefs.Save();
            CharacterUIController.Instance.ResetCharacterPoints();
            CharacterUIController.Instance.UpdateCharacterPoints(characterSpecial[selectedTabItem.transform.GetSiblingIndex()]);
        }
    }
  
    // Start is called before the first frame update
    private void Awake()
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
        selectedTabItem.transform.localScale = new Vector3(1.1f, 1f, 1f);
        MenuController.Instance.characterPrefs.SetString("character", "Sword");
        MenuController.Instance.characterPrefs.Save();
    }

    private void OnEnable()
    {
        CharacterUIController.Instance.ResetCharacterPoints();
        CharacterUIController.Instance.UpdateCharacterPoints(characterSpecial[selectedTabItem.transform.GetSiblingIndex()]);
    }
}
