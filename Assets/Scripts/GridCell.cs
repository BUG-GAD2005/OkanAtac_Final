using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    //public bool isHighlighted;
    [HideInInspector] public bool isOccupied;
    [HideInInspector] public bool isHighlightedRed;
    [HideInInspector] public bool isHighlightedGreen;
    [HideInInspector] public Building building;

    void Start()
    {
        isOccupied = false;
        isHighlightedRed = false;
        isHighlightedGreen = false;
    }
     
    public void occupyCell(Building buildingInstance)
    {
        isOccupied = true;
        building = buildingInstance;
    }

    public void highlightCell(RaycastHit2D hit)
    {
        if (isOccupied)
        {
            hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            isHighlightedRed = true;
        }
        else
        {
            hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            isHighlightedGreen = true;
        }
    }

    public void unhighlightCell()
    {
        if (isHighlightedRed)
        {
            building.buildingSprite.color = Color.red;
            isHighlightedRed = false;
        }
        else if (isHighlightedGreen)
        {
            building.buildingSprite.color = Color.green;
            isHighlightedGreen = false;
        }
    }

    public bool canOccupyCell()
    {
        return !isOccupied;
    }

}