using UnityEngine;

public class GridManager : MonoBehaviour
{
    const int numRows = 10;
    const int numColumns = 10;
    public GameObject cellPrefab;

    void Start()
    {
        RemoveChildren();
        // change aspect wrto screen size, 1080p
        Camera.main.aspect = 1920f / 1080f;
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

        for (int row = 0; row < numRows; row++)
        {   
            for (int col = 0; col < numColumns; col++)
            {
                // Calculate the position of each cell based on row and column values
                Vector2 position = new Vector2(col * spacingX, row * spacingY);

                float rightAlignOffset = (Camera.main.orthographicSize / 2f * Camera.main.aspect) - (scaledCellWidth / 2f);
                
                // Apply the starting offset
                position.x += startX + rightAlignOffset;
                position.y -= startY;

                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);

                cell.transform.SetParent(transform);

            }
        }
    }
    
    void RemoveChildren()
	{
		int childCount = this.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i)
		{
			GameObject.Destroy(this.transform.GetChild(i).gameObject);
		}
	}
}