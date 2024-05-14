using UnityEngine.UI;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int fatigue;
    public int maxHP;
    public int currentHP;
    public int maxEnergy;
    public int currentEnergy;


  
    // Method to take damage
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    // Method to take fatigue
    public bool TakeFatigue(int nrg)
    {
        currentEnergy -= nrg;

        if (currentEnergy <= 0)
            return true;
        else
            return false;
    }

    // Method to heal
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    // Method to replenish energy
    public void Replenish(int amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy)
            currentEnergy = maxEnergy;
    }

    // Method to decrease energy
    public void DecreaseEnergy(int amount)
    {
        currentEnergy -= amount;

        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
    }

    // Method to get random damage
    public int GetRandomDamage(int minDamage, int maxDamage)
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    // Method to get random fatigue
    public int GetRandomFatigue(int minFatigue, int maxFatigue)
    {
        return Random.Range(minFatigue, maxFatigue + 1);
    }
}
