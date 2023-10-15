using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RerollButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float width;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipController.Instance.ShowTooltip("-100", width);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipController.Instance.HideTooltip();
    }
}
