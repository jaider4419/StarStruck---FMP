using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 originalPosition;
    private RectTransform rectTransform;

    public string batteryType;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform.SetAsLastSibling();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (!IsDroppedIntoSlot())
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }

    private bool IsDroppedIntoSlot()
    {
        ItemSlot[] slots = GameObject.FindObjectsOfType<ItemSlot>();
        foreach (ItemSlot slot in slots)
        {
            RectTransform slotRectTransform = slot.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(slotRectTransform, rectTransform.position))
            {
                return true;
            }
        }
        return false;
    }
}
