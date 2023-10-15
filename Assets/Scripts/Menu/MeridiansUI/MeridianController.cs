using TMPro;
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
    //Singleton variable
    public static MeridianController Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
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
