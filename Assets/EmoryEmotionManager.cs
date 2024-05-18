using UnityEngine;
using UnityEngine.UI;

public class EmoryEmotionManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterEmotionData
    {
        public Slider energySlider;
        public Slider healthSlider;  // Add the health slider here
        public Image emotionImage;
        public Sprite happySprite;
        public Sprite neutralSprite;
        public Sprite tiredSprite;
        public Sprite friedSprite;
        public Image portraitImage; // Add the portrait image component here
        public Sprite happyPortrait;
        public Sprite neutralPortrait;
        public Sprite tiredPortrait;
        public Sprite friedPortrait;
    }

    public CharacterEmotionData[] characters;

    private void Start()
    {
        foreach (CharacterEmotionData character in characters)
        {
            if (character.energySlider != null && character.healthSlider != null)
            {
                // Initialize the energy slider to start at 15f and health slider to full
                character.energySlider.value = 15f;
                character.healthSlider.value = character.healthSlider.maxValue;
            }
            else
            {
                Debug.LogError("Energy or health slider is not assigned for a character.");
            }
        }
    }

    private void Update()
    {
        foreach (CharacterEmotionData character in characters)
        {
            if (character.energySlider != null && character.healthSlider != null)
            {
                UpdateEmotion(character);
                UpdatePortrait(character); // Call the UpdatePortrait method
            }
            else
            {
                Debug.LogError("Energy or health slider is not assigned for a character.");
            }
        }
    }

    private void UpdateEmotion(CharacterEmotionData character)
    {
        float energyLevel = character.energySlider.value;
        float healthLevel = character.healthSlider.value;
        Debug.Log("Energy Level: " + energyLevel); // Debug log to check the energy level value
        Debug.Log("Health Level: " + healthLevel); // Debug log to check the health level value

        if (healthLevel <= 0f || energyLevel <= 0f)
        {
            character.emotionImage.sprite = character.friedSprite;
        }
        else if (energyLevel <= 6.25f)
        {
            character.emotionImage.sprite = character.tiredSprite;
        }
        else if (energyLevel <= 15f)
        {
            character.emotionImage.sprite = character.neutralSprite;
        }
        else
        {
            character.emotionImage.sprite = character.happySprite;
        }
    }

    private void UpdatePortrait(CharacterEmotionData character)
    {
        float energyLevel = character.energySlider.value;
        float healthLevel = character.healthSlider.value;

        if (healthLevel <= 0f || energyLevel <= 0f)
        {
            character.portraitImage.sprite = character.friedPortrait;
        }
        else if (energyLevel <= 6.25f)
        {
            character.portraitImage.sprite = character.tiredPortrait;
        }
        else if (energyLevel <= 15f)
        {
            character.portraitImage.sprite = character.neutralPortrait;
        }
        else
        {
            character.portraitImage.sprite = character.happyPortrait;
        }
    }
}
