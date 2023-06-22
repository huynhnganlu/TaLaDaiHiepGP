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
            parentCanvasGroup.alpha = 0.8f;
    }

    public void closeUIPanel()
    {
        UIPanel.SetActive(!UIPanel.activeSelf);
        parentCanvasGroup.interactable = true;
        parentCanvasGroup.alpha = 1f;
    }

}
