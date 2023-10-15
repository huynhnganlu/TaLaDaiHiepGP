using TMPro;
using UnityEngine;

public class PropertyDataController : MonoBehaviour
{
    public TextMeshProUGUI property, value;
    //Set du lieu cho property meridian
    public void SetData(string property, string value)
    {
        this.property.text = property;
        this.value.text = value;
    }
}
