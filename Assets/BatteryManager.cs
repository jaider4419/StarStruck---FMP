using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    public ItemSlot[] slots;
    public GameObject winUI;

    private void Update()
    {
        if (CheckWinCondition())
        {
            ShowWinUI();
        }
    }

    private bool CheckWinCondition()
    {
        // Check if all slots are occupied and batteries are in correct slots
        foreach (ItemSlot slot in slots)
        {
            if (!slot.IsSlotOccupied() || !slot.IsCorrectBatteryInSlot())
            {
                return false; // At least one slot is not occupied or has incorrect battery
            }
        }
        return true; // All slots are occupied and have correct batteries
    }

    private void ShowWinUI()
    {
        winUI.SetActive(true);
    }
}
