using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(GridLayoutGroup))]
public class GridLayoutGroupFix : MonoBehaviour
{
   
    public enum RatioMode { Free, Fixed };
    [SerializeField] RatioMode ratioMode;
    [SerializeField] float cellRatio = 1;

    private new RectTransform transform;
    private GridLayoutGroup grid;

    // Start is called before the first frame update
    void Start()
    {
        transform = (RectTransform)base.transform;
        grid = GetComponent<GridLayoutGroup>();
        UpdateCellSize();
    }

    void OnRectTransformDimensionsChange()
    {
        UpdateCellSize();
    }

    void UpdateCellSize()
    {   
        if (grid != null)
        {
            var count = grid.constraintCount;
            float spacing = (count - 1) * grid.spacing.x;
            float contentSize = transform.rect.width - grid.padding.left - grid.padding.right - spacing;
            float sizePerCell = contentSize / count;
            grid.cellSize = new Vector2(sizePerCell, ratioMode == RatioMode.Free ? grid.cellSize.y : sizePerCell * cellRatio);
        }
        
       
    }
}
