using System.Collections;
using System.Collections.Generic;
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

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool TakeFatigue(int nrg)
    {
        currentEnergy -= nrg;

        if (currentEnergy <= 0) 
            return true;
        else
            return false;  
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void Replenish(int amount)
    {
        currentHP += amount;
        if (currentEnergy < maxEnergy)
            currentEnergy = maxEnergy;
    }

    public int GetRandomDamage(int minDamage, int maxDamage)
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    public int GetRandomFatigue(int minFatigue, int maxFatigue)
    {
       return Random.Range(minFatigue, maxFatigue + 1);
    }

}
