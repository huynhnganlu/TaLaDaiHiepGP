using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabItem : MonoBehaviour, IPointerClickHandler
{
    public TabGroupAbstract tabGroup;
    public Image background;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
        AudioManager.Instance.PlaySE("ClickButtonSE");
    }

    void Start()
    {
        if (GetComponent<MeridianAbstract>() != null)
        {
            tabGroup = MeridianController.Instance.meridianTabGroup;
        }
        background = GetComponent<Image>();
        tabGroup.AddTabItems(this);
    }
}
