using System;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] public Building[] buildings;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private SaveManager saveManager;


    void Start()
    {
        InitializeCostsUI();
        InitializeNameUI();
        InitializeEarnUI();
    }


    private void OnApplicationQuit() {
        saveManager.SaveGame();
    }



    void Update()
    {
        for(int i = 0; i < buildings.Length; i++)
        {
           // check resources
            if(!playerData.checkResources(buildings[i].gemCost, buildings[i].goldCost))
            {
                buildings[i].buildingSprite.color = Color.red;
            }
            else
            {
                buildings[i].buildingSprite.color = Color.white;
            }
        }
    }

    void InitializeCostsUI()
    {
        foreach (Building building in buildings)
        {
            building.gemCostText.text = building.gemCost.ToString();
            building.goldCostText.text = building.goldCost.ToString();
        }
    }
    void InitializeEarnUI()
    {
        foreach (Building building in buildings)
        {
            building.gemEarnText.text = building.gemEarn.ToString();
            building.goldEarnText.text = building.goldEarn.ToString();
        }
    }

    void InitializeNameUI()
    {
        foreach (Building building in buildings)
        {
            building.buildingNameText.text = building.buildingName;
        }
    }
}
