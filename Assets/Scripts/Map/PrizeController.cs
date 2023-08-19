using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeController : MonoBehaviour
{
    
    public bool isFreezing = false;
    public static PrizeController Instance { get; private set; }
    [SerializeField]
    private GameObject[] prizePanels;
    public GameObject bg;

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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void togglePrize()
    {
        bg.SetActive(true);
        isFreezing = true;
        Time.timeScale = 0;
    }

   
}
