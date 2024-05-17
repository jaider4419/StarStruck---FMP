using UnityEngine;
using UnityEngine.UI;

public class EmotionManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterEmotionData
    {
        public Slider energySlider;
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

    private void Update()
    {
        foreach (CharacterEmotionData character in characters)
        {
            UpdateEmotion(character);
            UpdatePortrait(character); // Call the UpdatePortrait method
        }

    }

    private void UpdateEmotion(CharacterEmotionData character)
    {
        float energyLevel = character.energySlider.value;
        if (energyLevel >= 15f)
        {
            character.emotionImage.sprite = character.happySprite;
        }
        else if (energyLevel >= 6.25f)
        {
            character.emotionImage.sprite = character.neutralSprite;
        }
        else if (energyLevel >= 3.125f)
        {
            character.emotionImage.sprite = character.tiredSprite;
        }
        else if (energyLevel <= 0f)
        {
            character.emotionImage.sprite = character.friedSprite;
        }
    }

    private void UpdatePortrait(CharacterEmotionData character)
    {
        float energyLevel = character.energySlider.value;
        if (energyLevel >= 15f)
        {
            character.portraitImage.sprite = character.happyPortrait;
        }
        else if (energyLevel >= 6.25f)
        {
            character.portraitImage.sprite = character.neutralPortrait;
        }
        else if (energyLevel >= 3.125f)
        {
            character.portraitImage.sprite = character.tiredPortrait;
        }
        else if (energyLevel <= 0f)
        {
            character.portraitImage.sprite = character.friedPortrait;
        }
    }
}
