using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private bool isOccupied = false;
    private DragDrop currentBattery;

    public string expectedBatteryType;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DragDrop draggedItem = eventData.pointerDrag.GetComponent<DragDrop>();

            if (!isOccupied || draggedItem != currentBattery)
            {
                if (currentBattery != null)
                {
                    currentBattery.ResetPosition();
                    currentBattery = null;
                }

                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                isOccupied = true;
                currentBattery = draggedItem;
            }
        }
    }

    public bool IsSlotOccupied()
    {
        return isOccupied;
    }

    public bool IsCorrectBatteryInSlot()
    {
        if (currentBattery != null)
        {
            return currentBattery.batteryType == expectedBatteryType;
        }
        return false;
    }
}
