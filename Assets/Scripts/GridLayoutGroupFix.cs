using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutGroupFix : MonoBehaviour
{
    [SerializeField]
    private GameObject container;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        float width = container.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2(width / 2, width / 2);
        container.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
