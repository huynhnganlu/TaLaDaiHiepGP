using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUIPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject prePanel, nextPanel;

    public void OpenPanel()
    {
        nextPanel.SetActive(true);
        prePanel.SetActive(false);
        if (gameObject.name.Equals("SelectMapButton"))
        {
            MenuController.Instance.characterPrefs.SetString("character", CharacterTabGroup.selectedTabItem.gameObject.name);
            MenuController.Instance.characterPrefs.Save();
        }
    }

    public void ClosePanel()
    {
        prePanel.SetActive(true);
        nextPanel.SetActive(false);
    }
}
