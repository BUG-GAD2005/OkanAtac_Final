using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private string buildingName;
    public string BuildingName => buildingName;
    [SerializeField] private int gemCost;
    public int GemCost => gemCost;
    [SerializeField] private Text gemCostText;
    public Text GemCostText => gemCostText;
    [SerializeField] private int goldCost;
    public int GoldCost => goldCost;
    [SerializeField] private Text goldCostText;
    public Text GoldCostText => goldCostText;
    [SerializeField] private int goldEarn;
    public int GoldEarn => goldEarn;
    [SerializeField] private int gemEarn;
    public int GemEarn => gemEarn;
    [SerializeField] private float earnDuration;
    public float EarnDuration => earnDuration;
    [SerializeField] public SpriteRenderer buildingSprite;
    [SerializeField] private GameObject buildingShape;
    private Vector2 startPosition;
    private bool isDragging = false;
    [SerializeField]  public PlayerData playerData;

    private void Update()
    {
        if (isDragging)
        {
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
                    if(playerData.DeductResources(GoldCost, GemCost) == false){
                        CancelPlacement();
                        return;
                    }

                    Vector2 center = Vector2.zero;
                    foreach (GridCell cell in cells)
                    {
                        center += new Vector2(cell.transform.position.x, cell.transform.position.y);
                        cell.OccupyCell(this);
                        cell.HighlightCellOccupied();
                    }
                    buildingShape.transform.position = new Vector2(cells[0].transform.position.x, cells[0].transform.position.y);
                    isDragging = false;              
                    GridCell.ResetAll();
                    StartCoroutine(EarnResources());
                    return;
                }
            }
        }
    }

    public IEnumerator EarnResources()
    {
        while (true)
        {
            float earnInterval = EarnDuration;
            yield return new WaitForSeconds(earnInterval);

            int total = GridCell.GetOccupiedCardCount(BuildingName);
            int gemEarned = GemEarn * total;
            int goldEarned = GoldEarn * total;

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

    public void CancelPlacement()
    {
        isDragging = false;
        buildingShape.SetActive(false);
        GridCell.ResetAll();
    }

    private void OnMouseDown()
    {
        if (playerData.checkResources(GoldCost, GemCost) == false)
        {
            return;
        }
        GridCell.HighlightAll();
        StartDragging();
    }
}
