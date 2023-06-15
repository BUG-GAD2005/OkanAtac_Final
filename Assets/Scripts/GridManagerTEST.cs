using UnityEngine;

public class GridManagerTEST : MonoBehaviour
{
    const int numRows = 10;
    const int numColumns = 10;
    public GameObject cellPrefab;

    void Start()
    {
        // change aspect wrto screen size
        Camera.main.aspect = Screen.width / Screen.height;
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Vector2 cellSize = cellPrefab.GetComponent<SpriteRenderer>().bounds.size;

        float gridWidth = numColumns * cellSize.x;
        float gridHeight = numRows * cellSize.y;

        float scaleFactor = 1f;

        if (gridWidth > Screen.width || gridHeight > Screen.height)
        {
            float scaleX = Screen.width / gridWidth;
            float scaleY = Screen.height / gridHeight;
            scaleFactor = Mathf.Min(scaleX, scaleY);
        }

        float scaledCellWidth = cellSize.x * scaleFactor;
        float scaledCellHeight = cellSize.y * scaleFactor;
       

        float spacingX = scaledCellWidth; 
        float spacingY = scaledCellHeight;

        float startX = -(gridWidth / 2f) + (scaledCellWidth / 2f);
        float startY = (gridHeight / 2f) - (scaledCellHeight / 2f);

        Debug.Log(startX);

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numColumns; col++)
            {
                // Calculate the position of each cell based on row and column values
                Vector2 position = new Vector2(col * spacingX, row * spacingY);

                // Apply the starting offset
                position.x += startX;
                position.y -= startY;

                position.x +=  (Camera.main.orthographicSize / 2f * Camera.main.aspect) - (scaledCellWidth / 2f);

                // Instantiate a cell GameObject at the calculated position
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);

                // Set the parent of the cell GameObject to the grid container
                cell.transform.SetParent(transform);
                float newScaleFactorWRTOScreen = Camera.main.orthographicSize * 2 / Screen.height;
                float newScaleFactorWRTOScreen2 = Camera.main.orthographicSize * 2 * Camera.main.aspect / Screen.width;
                cell.transform.localScale = new Vector2(newScaleFactorWRTOScreen, newScaleFactorWRTOScreen2);
            }
        }
    }
}