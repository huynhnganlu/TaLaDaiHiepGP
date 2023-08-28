using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeridianController : MonoBehaviour
{
    [SerializeField]
    private Button levelUpMeridianButton;
    [SerializeField]
    private MeridianTabGroup meridianTabGroup;
    [SerializeField]
    private TextMeshProUGUI levelMeridianUI;
    [SerializeField]
    private GameObject meridianInfoDataHolder;

    public static MeridianController Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        levelUpMeridianButton.onClick.AddListener(() =>
        {
            meridianTabGroup.selectedTabItem.GetComponent<MeridianAbstract>().levelUpMeridian();
        });
    }
    public void SetMeridianLevel(int level)
    {
        levelMeridianUI.text = level + "/180";
    }
}
