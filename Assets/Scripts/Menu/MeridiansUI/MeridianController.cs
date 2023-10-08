using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MeridianController : MonoBehaviour
{
    //Process meridian variables
    public Button levelUpMeridianButton;
    public MeridianTabGroup meridianTabGroup;
    public GameObject parentPropertyData;
    [SerializeField]
    private TextMeshProUGUI levelMeridianUI;
    //Qi to level up merdian variable
    public TextMeshProUGUI qiHolder;
    public Image imageHolder;
    //Character data variable
    [SerializeField]
    private CharacterData characterData;
    //Singleton variable
    public static MeridianController Instance;
    //PlayerPref variable
    public JsonPlayerPrefs meridianPrefs;
    public JsonPlayerPrefs characterPrefs;

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

        meridianPrefs = MenuController.Instance.meridianPrefs;
        characterPrefs = MenuController.Instance.characterPrefs;
    }


    private void Start()
    {
              
        //Set value of Qi
        qiHolder.text = characterData.qi.ToString();
        //Assign ham level up vao level up button
        levelUpMeridianButton.onClick.AddListener(() =>
        {
            meridianTabGroup.selectedTabItem.GetComponent<MeridianAbstract>().LevelUpMeridian();
        });
       
    }

    //Set UI level
    public void SetMeridianLevel(int level)
    {
        levelMeridianUI.text = level + "/36";
    }
   
}
