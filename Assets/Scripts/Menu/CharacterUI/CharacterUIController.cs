using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIController : MonoBehaviour
{
    public static CharacterUIController Instance;

    //inner ui holder variable
    [SerializeField]
    private Sprite imageNull;
    [SerializeField]
    private GameObject innerUIHolder;
    [SerializeField]
    private InnerHolder innerHolder;
    public JsonPlayerPrefs prefsShop;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    //Khoi tao UI khi da trang bi
    public void CreateEquipedInventoryItemsUI()
    {
        prefsShop = new JsonPlayerPrefs(Application.persistentDataPath + "/ShopData.json");
        for (int i = 0;i <= 2; i++)
        {
            if(prefsShop.GetInt("slot"+i) == -1)
            {
                innerUIHolder.transform.GetChild(i).GetComponentsInChildren<Image>().Skip(2).First().sprite = imageNull;
            }
            else
            {
                innerUIHolder.transform.GetChild(i).GetComponentsInChildren<Image>().Skip(2).First().sprite = innerHolder.listInner[prefsShop.GetInt("slot"+i)].GetComponent<ShopDataAbstract>().itemImage;
            }
        }
    }
}
