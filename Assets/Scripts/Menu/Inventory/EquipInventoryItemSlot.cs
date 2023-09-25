using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipInventoryItemSlot : MonoBehaviour, IPointerClickHandler
{
    private JsonPlayerPrefs shopPrefs;

    private void OnEnable()
    {
        shopPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/ShopData.json");
        Image childImage = transform.GetChild(0).GetComponent<Image>();
        if (shopPrefs.GetInt("slot" + transform.GetSiblingIndex()) != -1)
        {
            childImage.sprite = InventoryController.Instance.innerHolder.listInner[shopPrefs.GetInt("slot" + transform.GetSiblingIndex())].GetComponent<ShopDataAbstract>().itemImage;
            childImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            childImage.sprite = null;
            childImage.color = new Color(1f, 1f, 1f, 0f);
        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryController.Instance.CloseEquipUI();
        InventoryController.Instance.EquipItem(transform.GetSiblingIndex());
    }
}
