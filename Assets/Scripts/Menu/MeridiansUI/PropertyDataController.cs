using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PropertyDataController : MonoBehaviour
{
    public TextMeshProUGUI property, value;

    public void SetData(string property, string value)
    {
        this.property.text = property;
        this.value.text = value;
    }
}
