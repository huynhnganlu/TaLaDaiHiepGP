using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TooltipController : MonoBehaviour
{
    [SerializeField]
    private Camera uiCamera;
    [SerializeField]
    private RectTransform tooltipBG;
    [SerializeField]
    private TextMeshProUGUI tooltipText;

    public static TooltipController Instance;

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
    }

    public void ShowTooltip(string textShow)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out Vector2 localPoint);
        transform.localPosition = localPoint;
        tooltipText.text = textShow;
        gameObject.SetActive(true);
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
