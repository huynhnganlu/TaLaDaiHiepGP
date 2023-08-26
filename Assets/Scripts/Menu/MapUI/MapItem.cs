using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapItem : MonoBehaviour, IPointerClickHandler
{
    public string mapDescription;
    public TextMeshProUGUI textUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        textUI.text = mapDescription;
        MapController.Instance.currentMap = name;
    }
}
