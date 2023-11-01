using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScale : MonoBehaviour
{
    public float width, height;
    public void OnMouseEnterButton()
    {
        transform.localScale = new Vector3(width, height, 1f);
    }
    public void OnMouseExitButton()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
