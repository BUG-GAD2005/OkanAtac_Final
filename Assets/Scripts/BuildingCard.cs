using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform cardRectTransform;
    private RectTransform imageRectTransform;
    private Image cardImage;
    private Vector2 originalPosition;

    private void Awake()
    {
        //cardRectTransform = GetComponent<RectTransform>();
        imageRectTransform = transform.Find("HomeCardImage").GetComponent<RectTransform>();
        cardImage = transform.Find("HomeCardImage").GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = imageRectTransform.position;
        cardImage.color = new Color(1f, 1f, 1f, 0.7f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        imageRectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        imageRectTransform.position = originalPosition;
        cardImage.color = new Color(1f, 1f, 1f, 1f);

        if (eventData.pointerEnter != null)
        {
            GameObject targetObject = eventData.pointerEnter;

            // TODO: Check if the card was dropped on a valid target object
        }
    }
}
