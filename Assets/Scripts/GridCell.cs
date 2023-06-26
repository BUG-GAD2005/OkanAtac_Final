using UnityEngine;

public class GridCell : MonoBehaviour
{
    public bool IsOccupied { get; private set; }
    public Building Building { get; private set; }
    private SpriteRenderer cellSpriteRenderer;

    private void Awake()
    {
        cellSpriteRenderer = GetComponent<SpriteRenderer>();
        ResetCellState();
    }

    public void OccupyCell(Building buildingInstance)
    {
        IsOccupied = true;
        Building = buildingInstance;
    }

    public void HighlightCellGreen()
    {
        cellSpriteRenderer.color = Color.green;
    }

    public void HighlightCellRed()
    {
        cellSpriteRenderer.color = Color.red;
    }

    public void HighlightCellOccupied()
    {
        cellSpriteRenderer.sprite = Resources.Load<Sprite>("Assets/Imported/tile-building.png");
    }

    public void ResetCellColor()
    {
        cellSpriteRenderer.color = Color.white;
    }

    public bool CanOccupyCell()
    {
        return !IsOccupied;
    }

    public void ResetCellState()
    {
        IsOccupied = false;
        Building = null;
        cellSpriteRenderer.color = Color.white;
    }

    public static  void HighlightAll()
    {
        GridCell[] cells = FindObjectsOfType<GridCell>();
        foreach(GridCell cell in cells)
        {
            if (cell.IsOccupied)
            {
                cell.HighlightCellOccupied();
            } 
            else
            {
                cell.HighlightCellGreen();
            }
        }
    }

    public static void ResetAllColor()
    {
        GridCell[] cells = FindObjectsOfType<GridCell>();
        foreach(GridCell cell in cells)
        {
            cell.ResetCellColor();
        }
    }

    public static int GetOccupiedCardCount(string name)
    {
        GridCell[] cells = FindObjectsOfType<GridCell>();
        int count = 0;
        foreach(GridCell cell in cells)
        {
            if (cell.IsOccupied && cell.Building.buildingName == name)
            {
                count++;
            }
        }
        return count;
    }
}
