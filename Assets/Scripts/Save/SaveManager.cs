using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    private string savePath;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private GridCell[] cells;

    [SerializeField] private BuildingManager BuildingManager;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/save8.dat";
    }

    private void Start()
    {
        cells = FindObjectsOfType<GridCell>();
        if (File.Exists(savePath))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(savePath, FileMode.Create))
        {
            SaveData data = new SaveData();

            data.gold = playerData.gold;
            data.gems = playerData.gems;

            data.cells = new List<SaveData.CellData>();
            data.buildings = new List<SaveData.BuildingData>();

            foreach (GridCell cell in cells)
            {
                data.cells.Add(ConvertGridCellToCellData(cell));
                if (cell.IsOccupied)
                    data.buildings.Add(ConvertBuildingToBuildingData(cell.Building));
            }


            formatter.Serialize(stream, data);
        }
    }

    public void LoadGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(savePath, FileMode.Open))
        {
            SaveData data = formatter.Deserialize(stream) as SaveData;
    
            playerData.gold = data.gold;
            playerData.gems = data.gems;
            
            foreach (SaveData.CellData cellData in data.cells)
            {
                Vector2 cellDataPosition = cellData.position;
                GridCell cell = Array.Find(cells, c => new Vector2(c.transform.position.x, c.transform.position.y) == cellDataPosition);
                if (cell != null)
                {
                    ApplyCellDataToGridCell(cellData, cell);

                    string buildingName = cellData.occupiedBuildingName;
                    if (buildingName != null)
                    {
                        Building building = Array.Find(BuildingManager.buildings, b => b.buildingName == buildingName);
                        if (building != null)
                        {
                            Building newBuilding = Instantiate(building, cell.transform.position, Quaternion.identity);
                            newBuilding.transform.position = cell.transform.position;
                            newBuilding.transform.localScale = new Vector3(1, 1, 1);
                            StartCoroutine(newBuilding.EarnResources());
                        }
                    }
                }
            }
        }
    }

    private SaveData.CellData ConvertGridCellToCellData(GridCell cell)
    {
        return new SaveData.CellData
        {
            position = new Vector2(cell.transform.position.x, cell.transform.position.y),
            isOccupied = cell.IsOccupied,
            occupiedBuildingName = cell.IsOccupied ? cell.Building.buildingName : null
        };
    }

    private SaveData.BuildingData ConvertBuildingToBuildingData(Building building)
    {
        return new SaveData.BuildingData
        {
            name = building.buildingName
        };
    }

    private void ApplyCellDataToGridCell(SaveData.CellData cellData, GridCell cell)
    {
        if (cellData.isOccupied)
        {
            Building occupyingBuilding = Array.Find(BuildingManager.buildings, b => b.buildingName == cellData.occupiedBuildingName);
            if (occupyingBuilding != null)
            {
                cell.OccupyCell(occupyingBuilding);
                cell.HighlightCellOccupied();
            }
        }
        else
        {
            cell.ResetCellState();
        }
    }

    public void ResetSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
}