using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Slider hpSlider;
    public Slider energySlider;
    public Image playerHUDFrame;
    public Image glowEffect;

    // Method to set the HUD for the player
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        energySlider.maxValue = unit.maxEnergy; // Fixed typo: currentEnergy -> maxEnergy
        energySlider.value = unit.currentEnergy;

        if (glowEffect != null)
        {
            glowEffect.enabled = false;
        }
    }

    // Method to toggle the glow effect based on the current turn
    public void ToggleGlow(bool shouldGlow)
    {
        if (glowEffect != null)
        {
            glowEffect.enabled = shouldGlow;
        }
    }

    // Method to set the HP value of the HUD
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    // Method to set the energy value of the HUD
    public void SetEnergy(int energy)
    {
        energySlider.value = energy;
    }
}
