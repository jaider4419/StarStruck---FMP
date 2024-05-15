using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private bool isOccupied = false;
    private DragDrop currentBattery;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DragDrop draggedItem = eventData.pointerDrag.GetComponent<DragDrop>();

            // Check if the slot is not occupied
            if (!isOccupied)
            {
                // Snap the battery to this slot
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                // Update slot status and current battery
                isOccupied = true;
                currentBattery = draggedItem;
            }
            else
            {
                // If the slot is occupied, reset the battery's position
                draggedItem.ResetPosition();
            }
        }
    }

    public bool IsSlotOccupied()
    {
        return isOccupied;
    }
}
