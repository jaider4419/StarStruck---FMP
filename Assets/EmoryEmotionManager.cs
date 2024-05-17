using UnityEngine;
using UnityEngine.UI;

public class EmoryEmotionManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterEmotionData
    {
        public Slider energySlider;
        public Image emotionImage;
        public Sprite neutralSprite;
        public Sprite tiredSprite;
        public Sprite friedSprite;
        public Image portraitImage; 
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
            UpdatePortrait(character); 
        }

    }

    private void UpdateEmotion(CharacterEmotionData character)
    {
        float energyLevel = character.energySlider.value;
        if (energyLevel >= 15f)
        {
            character.emotionImage.sprite = character.neutralSprite;
        }
        else if (energyLevel > 6.25f)
        {
            character.emotionImage.sprite = character.tiredSprite;
        }
        else if (energyLevel > 0f)
        {
            character.emotionImage.sprite = character.friedSprite;
        }
    }

    private void UpdatePortrait(CharacterEmotionData character)
    {
        float energyLevel = character.energySlider.value;
        if (energyLevel >= 15f)
        {
            character.portraitImage.sprite = character.neutralPortrait;
        }
        else if (energyLevel > 6.25f)
        {
            character.portraitImage.sprite = character.tiredPortrait;
        }
        else if (energyLevel > 0f)
        {
            character.portraitImage.sprite = character.friedPortrait;
        }
    }
}
