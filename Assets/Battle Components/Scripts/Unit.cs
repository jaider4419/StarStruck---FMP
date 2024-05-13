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

    public GameObject emotionObject; // Reference to the emotion UI GameObject

    // Add sprites for different emotions
    public Sprite happySprite;
    public Sprite neutralSprite;
    public Sprite sadSprite;
    public Sprite friedSprite; // New sprite for when the unit is defeated

    void Update()
    {
        // Update emotion UI based on energy level
        UpdateEmotionUI();
    }

    // Method to update the emotion UI based on energy level
    void UpdateEmotionUI()
    {
        // Check if the unit is defeated
        if (currentHP <= 0)
        {
            SetEmotionSprite(friedSprite); // Set emotion to friedSprite when defeated
            return;
        }

        // Calculate energy percentage
        float energyPercentage = (float)currentEnergy / maxEnergy;

        // Set emotion based on energy level
        if (energyPercentage >= 0.5f)
        {
            SetEmotionSprite(happySprite);
        }
        else if (energyPercentage >= 0.25f)
        {
            SetEmotionSprite(neutralSprite);
        }
        else
        {
            SetEmotionSprite(sadSprite);
        }
    }

    // Method to set the emotion sprite
    void SetEmotionSprite(Sprite sprite)
    {
        // Get the Image component from the emotion object
        Image emotionImage = emotionObject.GetComponent<Image>();

        // Check if the Image component exists
        if (emotionImage != null)
        {
            emotionImage.sprite = sprite; // Set the sprite
        }
        else
        {
            Debug.LogError("EmotionObject does not have an Image component!");
        }
    }

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
