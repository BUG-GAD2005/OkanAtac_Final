using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Building[] buildings;

    void Start()
    {
        initializeCostsUI();
    }

    void initializeCostsUI()
    {
        foreach (Building building in buildings)
        {
            building.gemCostText.text = building.gemCost.ToString();
            building.goldCostText.text = building.goldCost.ToString();
        }
    }
}
