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
        // Check if all slots are occupied
        foreach (ItemSlot slot in slots)
        {
            if (!slot.IsSlotOccupied())
            {
                return false; // At least one slot is not occupied
            }
        }
        return true; // All slots are occupied
    }

    private void ShowWinUI()
    {
        winUI.SetActive(true);
    }
}
