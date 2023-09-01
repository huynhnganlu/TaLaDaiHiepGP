using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EquipInventoryItemsUI : MonoBehaviour
{

    [SerializeField]
    private Sprite imageNull, imageTest;

    //Khoi tao UI khi da trang bi
    public void CreateEquipedInventoryItemsUI(List<InventoryItems> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            GameObject slot = transform.GetChild(i).gameObject;
            if (list[i] != null)
            {
                slot.GetComponentsInChildren<Image>().Skip(1).First().sprite = imageTest;
            }
            else
            {
                slot.GetComponentsInChildren<Image>().Skip(1).First().sprite = imageNull;
            }
        }
    }

}
