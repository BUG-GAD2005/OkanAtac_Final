using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour //, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public string buildingName;
    [SerializeField] public int gemCost;
    [SerializeField] public Text gemCostText;
    [SerializeField] public int goldCost;
    [SerializeField] public Text goldCostText;
    [SerializeField] public int goldEarn;
    [SerializeField] public int gemEarn;
    [SerializeField] public float earnDuration;
    [SerializeField] public SpriteRenderer buildingSprite;
    [SerializeField] GameObject buildingShape;
    private Vector2 startPosition;
    private Transform buildingShapeTransform;
    private bool isDragging = false;

    private void Awake()
    {
        buildingShapeTransform = buildingShape.GetComponent<Transform>();
    }

    private void Update()
    {
        if (isDragging)
        {
            // Update the position of the dragged object based on the mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            buildingShape.transform.position = new Vector3(mousePosition.x, mousePosition.y, -0.5f);
            // Check if the dragged object is released on the grid
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Cell"))
            {
                Debug.DrawRay(mousePosition, mousePosition + new Vector2(10,10), Color.green, 3.0f);
                // Snap the dragged object to the grid position
                hit.collider.gameObject.GetComponent<GridCell>().highlightCell(hit);
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 snappedPosition = new Vector3(hit.transform.position.x, hit.transform.position.y, -0.5f);
                    buildingShape.transform.position = snappedPosition;
                    isDragging = false;
                }

                Debug.Log("Dropped on cell");
            }
                
                // Reset the dragging state    
        }
    }
    public void StartDragging(GameObject objectToDrag)
    {
        if (!isDragging)
        {
            isDragging = true;
            buildingShape.SetActive(true);
            buildingShape = objectToDrag;
            startPosition = objectToDrag.transform.position;
        }
    }

    private void OnMouseDown()
    {
        StartDragging(buildingShape);
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with cell");
        if (collision.gameObject.tag == "Cell")
        {
            
        }
    }*/


    /*public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = imageRectTransform.position;
        Debug.Log("picked up.");
        cardImage.color = new Color(1f, 1f, 1f, 0.7f);
    }*/

    /*public void OnDrag(PointerEventData eventData)
    {
        //set active
        buildingShape.SetActive(true);
        Debug.Log("building shape active.");
        buildingShapeRectTransform.position = eventData.position;
        // reduce scale
        buildingShapeRectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }*/

    /*public void OnEndDrag(PointerEventData eventData)
    {
        buildingShapeRectTransform.position = originalPosition;
        cardImage.color = new Color(1f, 1f, 1f, 1f);
        buildingShape.SetActive(false);
        
        /*if (eventData.pointerEnter != null)
        {
            GameObject targetObject = eventData.pointerEnter;
            // TODO: Check if the card was dropped on a valid target object
        }   
    }*/
}