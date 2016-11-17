using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIGridSizeFitter : MonoBehaviour
{

    public int minWidth = 0;
    public int minHeight = 0;

    private GridLayoutGroup grid;
    private int lastChildCount = 0;
    private RectTransform rectTransform;

    void Start()
    {
        grid = this.gameObject.GetComponent<GridLayoutGroup>();
        rectTransform = this.gameObject.GetComponent<RectTransform>();
        if (grid == null)
        {
            Debug.LogWarning(">> UIGridSizeFitter and GridLayoutGroup at the same GameObject.");
        }
    }

    void Update()
    {
        int childCount = 0;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.activeSelf)
            {
                childCount++;
            }
        }

#if UNITY_EDITOR
        if (grid != null)
#else
        if (grid != null && childCount != lastChildCount)
#endif
        {
            Calculate(childCount);
        }
    }

    public void Calculate(int childCount)
    {
        lastChildCount = childCount;
        float col = 1;
        float row = 1;
        if (grid.constraint == GridLayoutGroup.Constraint.Flexible)
        {
            if (grid.startAxis == GridLayoutGroup.Axis.Horizontal)//竖排
            {
                col = 1;
                row = lastChildCount;
            }
            else
            {
                col = lastChildCount;
                row = 1;
            }
        }
        else if (grid.constraint == GridLayoutGroup.Constraint.FixedRowCount)//startAxis属性暂未处理
        {
            row = grid.constraintCount;
            col = Mathf.CeilToInt(lastChildCount / row);
        }
        else if (grid.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            col = grid.constraintCount;
            row = Mathf.CeilToInt(lastChildCount / col);
        }

        float gridWidth = 0;
        float gridHeight = 0;
        float colGapCount = col - 1;
        float rowGapCount = row - 1;

        gridWidth = col * grid.cellSize.x + (colGapCount > 0 ? colGapCount : 0) * grid.spacing.x + grid.padding.left + grid.padding.right;
        gridHeight = row * grid.cellSize.y + (rowGapCount > 0 ? rowGapCount : 0) * grid.spacing.y + grid.padding.top + grid.padding.bottom;

        rectTransform.sizeDelta = new Vector2(Mathf.Max(gridWidth, minWidth), Mathf.Max(gridHeight, minHeight));
    }

}


