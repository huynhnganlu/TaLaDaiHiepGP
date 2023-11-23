using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RerollButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float width, x, y;
    [TextArea]
    public string text;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipController.Instance.ShowTooltip(text, width, x, y);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipController.Instance.HideTooltip();
    }
}
