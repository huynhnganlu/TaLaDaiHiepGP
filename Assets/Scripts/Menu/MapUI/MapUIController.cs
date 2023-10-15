using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapUIController : MonoBehaviour
{
    [SerializeField]
    private ScrollRect mapSelectScroll;
    private float scrollSpeed = 0.5f;
    public static MapUIController Instance { get; private set; }
    public string currentMap;
    public TextMeshProUGUI textUI;
    public GameObject[] mapHolder;
    private JsonPlayerPrefs characterPrefs;
    [SerializeField]
    private GameObject lockMap;
    [SerializeField]
    private TMP_Dropdown dropdown;
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

        characterPrefs = MenuController.Instance.characterPrefs;
        currentMap = "map1";
    }

    private void OnEnable()
    {
        ProcessMap();
    }

    public void HandleButtonScroll(string orientation)
    {

            if (orientation.Equals("left"))
            {
                if (mapSelectScroll.horizontalNormalizedPosition >= 0f)
                    mapSelectScroll.horizontalNormalizedPosition -= scrollSpeed;
            }
            else if (orientation.Equals("right"))
            {
                if (mapSelectScroll.horizontalNormalizedPosition <= 1f)
                    mapSelectScroll.horizontalNormalizedPosition += scrollSpeed;

            }        
    }

    public void ButtonStartFight()
    {
        characterPrefs.SetString("mapselected", currentMap);
        characterPrefs.SetInt("mapdiff", dropdown.value);
        characterPrefs.Save();
        SceneManager.LoadScene(currentMap);
    }
    public void ProcessMap()
    {
        if (characterPrefs.HasKey("map0"))
        {
            for(int i = 0; i <= 2; i++)
            {
                if(characterPrefs.GetInt("map"+ i) == 0)
                {
                    Instantiate(lockMap, mapHolder[i].transform);
                    mapHolder[i].GetComponent<Image>().raycastTarget = false;
                }
                else
                {
                    DestroyLock(mapHolder[i]);
                    mapHolder[i].GetComponent<Image>().raycastTarget = true;
                }
            }
        }
    }

    public void DestroyLock(GameObject go)
    {
        foreach(Transform children in go.transform)
            Destroy(children.gameObject);
    }
}
