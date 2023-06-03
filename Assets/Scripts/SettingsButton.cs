using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject settingsPanel;
    public CanvasGroup parentCanvasGroup;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openSettingsPanel()
    {      
            settingsPanel.SetActive(!settingsPanel.activeSelf);
            parentCanvasGroup.interactable = false;
            parentCanvasGroup.alpha = 0.8f;
    }

    public void closeSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        parentCanvasGroup.interactable = true;
        parentCanvasGroup.alpha = 1f;
    }

}
