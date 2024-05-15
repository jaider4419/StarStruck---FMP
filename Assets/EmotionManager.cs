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

    }

    public CharacterEmotionData[] characters;

    private void Update()
    {
        foreach (CharacterEmotionData character in characters)
        {
            UpdateEmotion(character);
        }

    }

    private void UpdateEmotion(CharacterEmotionData character)
    {
        float energyLevel = character.energySlider.value;
        if (energyLevel >= 25f)
        {
            character.emotionImage.sprite = character.happySprite;
        }
        else if (energyLevel >= 12.5f)
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
}
