using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RerollButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float width;
    public string text;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipController.Instance.ShowTooltip(text, width);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipController.Instance.HideTooltip();
    }
}
