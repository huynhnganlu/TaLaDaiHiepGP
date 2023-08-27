using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isDown = false;
    private string orientation;

    private void Start()
    {
        if(this.name.Equals("ButtonLeft"))
        {
            orientation = "left";
        }else if (this.name.Equals("ButtonRight"))
        {
            orientation = "right";
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        MapUIController.Instance.ScrollButtonClick(isDown, orientation);
        Debug.Log(orientation);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        MapUIController.Instance.ScrollButtonClick(isDown, orientation);
    }

    
}
