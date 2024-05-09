using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public Slider hpSlider;
    public Slider energySlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        energySlider.maxValue = unit.currentEnergy;
        energySlider.value = unit.maxHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetEnergy(int hp)
    {
        energySlider.value = hp;
    }

}
