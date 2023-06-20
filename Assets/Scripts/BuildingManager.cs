using System.Collections;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Building[] buildings;
    [SerializeField] PlayerData playerData;
    void Start()
    {
        InitializeCostsUI();
    }

    void InitializeCostsUI()
    {
        foreach (Building building in buildings)
        {
            building.GemCostText.text = building.GemCost.ToString();
            building.GoldCostText.text = building.GoldCost.ToString();
        }
    }

    void Update()
    {
        for(int i = 0; i < buildings.Length; i++)
        {
           // check resources
            if(!playerData.checkResources(buildings[i].GemCost, buildings[i].GoldCost))
            {
                buildings[i].buildingSprite.color = Color.red;
            }
            else
            {
                buildings[i].buildingSprite.color = Color.white;
            }
        }
    }
}
