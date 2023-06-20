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

    public void HighlightCell()
    {
        cellSpriteRenderer.color = Color.green;
    }

    public void HighlightCellOccupied()
    {
        cellSpriteRenderer.sprite = Resources.Load<Sprite>("Assets/Imported/tile-building.png");
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

    public void ResetCellColor()
    {
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
                cell.HighlightCell();
            }
        }
    }

    public static void ResetAll()
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
            if (cell.IsOccupied && cell.Building.BuildingName == name)
            {
                count++;
            }
        }
        return count;
    }
}
