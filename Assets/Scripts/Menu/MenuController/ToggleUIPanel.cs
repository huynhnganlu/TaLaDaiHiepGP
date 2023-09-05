using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleUIPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject UIPanel;
    [SerializeField]
    private CanvasGroup parentCanvasGroup;
    [SerializeField]
    private float alphaValue = 1f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openUIPanel()
    {      
            UIPanel.SetActive(!UIPanel.activeSelf);
            parentCanvasGroup.interactable = false;
            parentCanvasGroup.alpha = alphaValue;
            if (name.Equals("InventoryButton"))
                InventoryController.Instance.GetBoughtInner();
    }

    public void closeUIPanel()
    {
        UIPanel.SetActive(!UIPanel.activeSelf);
        parentCanvasGroup.interactable = true;
        parentCanvasGroup.alpha = 1f;
    }

}
