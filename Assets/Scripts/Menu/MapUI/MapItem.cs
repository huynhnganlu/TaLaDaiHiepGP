using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapItem : MonoBehaviour, IPointerClickHandler
{
    [TextAreaAttribute]
    [SerializeField]
    private string mapDescription;
    [SerializeField]
    private TextMeshProUGUI textHolder;

    public void OnPointerClick(PointerEventData eventData)
    {
        textHolder.text = mapDescription;
        MapUIController.Instance.currentMap = int.Parse(gameObject.name.Replace("map", ""));
        for(int i = 0; i < MapUIController.Instance.mapHolder.Length; i++)
        {
            if (MapUIController.Instance.mapHolder[i].name.Equals(gameObject.name))
            {
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                MapUIController.Instance.mapHolder[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.6f);
            }
        }
    }
}
