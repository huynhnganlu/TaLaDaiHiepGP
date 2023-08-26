using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EquipInventoryItemsUI : MonoBehaviour
{

    [SerializeField]
    private Image imageTest;

    //Khoi tao UI khi da trang bi
    public void CreateEquipedInventoryItemsUI(List<InventoryItems> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
            {
                GameObject slot = transform.GetChild(i).gameObject;
                slot.GetComponentsInChildren<Image>().Skip(1).First().sprite = imageTest.sprite;
                
                
            }
        }
    }

}
