using UnityEngine;

public class ToggleUIPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject UIPanel;
    [SerializeField]
    private CanvasGroup parentCanvasGroup;
 
    public void OpenUIPanel()
    {
        UIPanel.SetActive(!UIPanel.activeSelf);
        parentCanvasGroup.interactable = false;
    }

    public void CloseUIPanel()
    {
        UIPanel.SetActive(!UIPanel.activeSelf);
        parentCanvasGroup.interactable = true;
    }

}
