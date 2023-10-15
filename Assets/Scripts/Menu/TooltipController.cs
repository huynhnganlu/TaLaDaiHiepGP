using TMPro;
using UnityEngine;

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

    public void ShowTooltip(string textShow, float width)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out Vector2 localPoint);
        transform.localPosition = localPoint;
        tooltipText.text = textShow;
        tooltipText.GetComponent<RectTransform>().sizeDelta = new Vector2(width ,0);
        gameObject.SetActive(true);
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
