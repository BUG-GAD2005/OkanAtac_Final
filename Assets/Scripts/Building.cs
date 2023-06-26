using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public string buildingName;
    public Text buildingNameText;
    public int gemCost;
    public Text gemCostText;
    public int goldCost;
    public Text goldCostText;
    public int gemEarn;
    public Text gemEarnText;
    public int goldEarn;
    public Text goldEarnText;
    public float earningDuration;
    public SpriteRenderer buildingSprite;
    public GameObject buildingShape;
    public PlayerData playerData;

    private Vector2 startPosition;
    private bool isDragging = false;

    public void Update()
    {
        if (isDragging)
        {
            GridCell.ResetAllColor();
            highlightEligible();
            
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            buildingShape.transform.position = new Vector2(mousePosition.x, mousePosition.y);

            GridCell[] cells = new GridCell[buildingShape.transform.childCount];
            int cellIndex = 0;
            if (Input.GetMouseButtonDown(0))
            {
                foreach (Transform child in buildingShape.transform)
                {   
                    Vector2 childPosition = new Vector2(child.position.x, child.position.y);
                    RaycastHit2D[] hits = Physics2D.RaycastAll(childPosition, Vector2.zero);
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider != null && hit.collider.CompareTag("Cell"))
                        {   
                            GridCell gridCell = hit.collider.GetComponent<GridCell>();
                            if (gridCell.CanOccupyCell())
                            {
                                Vector2 snappedPosition = new Vector2(gridCell.transform.position.x, gridCell.transform.position.y);
                                cells[cellIndex] = gridCell;
                                cellIndex++;
                            }
                            else
                            {
                                CancelPlacement();
                                return;
                            }
                        }
                    }
                }
                if (cells.Length == cellIndex && cells.Length != 0)
                {
                    if(playerData.DeductResources(goldCost, gemCost) == false){
                        CancelPlacement();
                        return;
                    }

                    placeCell(cells);
                    GridCell.ResetAllColor();
                    StartCoroutine(EarnResources());
                }
            }
        }
    }

    public void placeCell(GridCell[] cells){
        Vector2 center = Vector2.zero;
        foreach (GridCell cell in cells)
        {
            center += new Vector2(cell.transform.position.x, cell.transform.position.y);
            cell.OccupyCell(this);
            cell.HighlightCellOccupied();
        }
        buildingShape.transform.position = new Vector2(cells[0].transform.position.x, cells[0].transform.position.y);
        isDragging = false;  
    }
    
    public void highlightEligible(){
        List<GridCell> potentialCells = GetPotentialCells();
        bool canPlace = true;
        foreach (GridCell gridCell in potentialCells)
        {
            if (!gridCell.CanOccupyCell())
                canPlace = false;
        }

        if (buildingShape.transform.childCount != potentialCells.Count)
            canPlace = false;
        

        foreach (GridCell gridCell in potentialCells)
        {
            if (canPlace)
                gridCell.HighlightCellGreen();
            else
                gridCell.HighlightCellRed();
        }
    }

    public IEnumerator EarnResources()
    {
        while (true)
        {
            float earnInterval = earningDuration;
            yield return new WaitForSeconds(earnInterval);

            int total = GridCell.GetOccupiedCardCount(buildingName);
            int gemEarned = gemEarn * total;
            int goldEarned = goldEarn * total;

            playerData.AddResources(goldEarned, gemEarned);
        }
    }

    public void StartDragging()
    {
        if (!isDragging)
        {
            isDragging = true;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            buildingShape = Instantiate(buildingShape, mousePosition, Quaternion.identity);
            buildingShape.SetActive(true);
            startPosition = mousePosition;

            GridCell[] gridCells = FindObjectsOfType<GridCell>();

        }
    }

    List<GridCell> GetPotentialCells()
    {
        List<GridCell> potentialCells = new List<GridCell>();

        foreach (Transform child in buildingShape.transform)
        {
            Vector2 childPosition = new Vector2(child.position.x, child.position.y);
            RaycastHit2D[] hits = Physics2D.RaycastAll(childPosition, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Cell"))
                {
                    GridCell gridCell = hit.collider.GetComponent<GridCell>();
                    potentialCells.Add(gridCell);
                }
            }
        }
        return potentialCells;
    }


    public void CancelPlacement()
    {
        isDragging = false;
        buildingShape.SetActive(false);
        GridCell.ResetAllColor();
    }

    private void OnMouseDown()
    {
        if (playerData.checkResources(goldCost, gemCost) == false)
        {
            return;
        }
        StartDragging();
    }
}
