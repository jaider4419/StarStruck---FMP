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

            
            if (!isOccupied)
            {
                
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                
                isOccupied = true;
                currentBattery = draggedItem;
            }
            else
            {
                
                draggedItem.ResetPosition();
            }
        }
    }

    public bool IsSlotOccupied()
    {
        return isOccupied;
    }
}
