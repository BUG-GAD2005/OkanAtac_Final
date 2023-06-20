using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public int gems;
    public int gold;
    public List<BuildingData> buildings;
    public List<CellData> cells;

    [System.Serializable]
    public struct BuildingData
    {
        public string name;
    }

    [System.Serializable]
    public struct CellData
    {
        public SerializableVector2 position;
        public bool isOccupied;
        public string occupiedBuildingName;
    }

}

[System.Serializable]
public struct SerializableVector2
{
    public float x;
    public float y;

    public SerializableVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static implicit operator Vector2(SerializableVector2 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static implicit operator SerializableVector2(Vector2 v)
    {
        return new SerializableVector2(v.x, v.y);
    }

    
}