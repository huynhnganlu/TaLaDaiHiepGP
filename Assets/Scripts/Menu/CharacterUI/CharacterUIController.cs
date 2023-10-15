using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIController : MonoBehaviour
{
    //singleton variable
    public static CharacterUIController Instance;

    //inner ui holder variable
    [SerializeField]
    private Sprite imageNull;
    [SerializeField]
    private GameObject innerUIHolder;
    [SerializeField]
    private InnerHolder innerHolder;

    //Pref variable
    private JsonPlayerPrefs characterPrefs;
    private JsonPlayerPrefs prefsShop;

    [SerializeField]
    private GameObject characterPoints, characterPointsParent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        prefsShop = MenuController.Instance.shopPrefs;
        characterPrefs = MenuController.Instance.characterPrefs;
    }

    private void OnEnable()
    {
        CreateEquipedInventoryItemsUI();
    }
    //Khoi tao UI khi da trang bi
    public void CreateEquipedInventoryItemsUI()
    {
        if (prefsShop.HasKey("slot1"))
        {
            for (int i = 0; i <= 2; i++)
            {
                if (prefsShop.GetInt("slot" + i) == -1)
                {
                    innerUIHolder.transform.GetChild(i).GetComponentsInChildren<Image>().Skip(2).First().sprite = imageNull;
                }
                else
                {
                    innerUIHolder.transform.GetChild(i).GetComponentsInChildren<Image>().Skip(2).First().sprite = innerHolder.listInner[prefsShop.GetInt("slot" + i)].GetComponent<ShopDataAbstract>().itemImage;
                }
            }
        }
    }

    public void ResetCharacterPoints()
    {
        foreach (Transform child in characterPointsParent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void UpdateCharacterPoints(Dictionary<string, int> characterSpecial)
    {
        foreach (KeyValuePair<string, string> property in MenuController.Instance.listCharacterProperty)
        {
            GameObject o = Instantiate(characterPoints, characterPointsParent.transform);
            TextMeshProUGUI[] textCompound = o.GetComponentsInChildren<TextMeshProUGUI>();
            if(property.Key.Equals("movementSpeed") || property.Key.Equals("critRate") || property.Key.Equals("evade"))
            {
                if (characterSpecial.ContainsKey(property.Key))
                    textCompound[1].text = (characterPrefs.GetFloat(property.Key) + characterSpecial[property.Key]).ToString();
                else
                    textCompound[1].text = characterPrefs.GetFloat(property.Key).ToString();
            }
            else
            {
                if (characterSpecial.ContainsKey(property.Key))
                    textCompound[1].text = (characterPrefs.GetInt(property.Key) + characterSpecial[property.Key]).ToString();
                else
                    textCompound[1].text = characterPrefs.GetInt(property.Key).ToString();
            }
            textCompound[0].text = property.Value;
        }
    }
}
