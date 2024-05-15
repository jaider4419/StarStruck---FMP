using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterPortraitData
    {
        public Slider energySlider;
        public Image portraitImage;
        public Sprite happyPortrait;
        public Sprite neutralPortrait;
        public Sprite tiredPortrait;
        public Sprite friedPortrait;

    }

    public CharacterPortraitData[] characters;

    private void Update()
    {
        foreach (CharacterPortraitData character in characters)
        {
            UpdatePortrait(character);
        }

    }

    private void UpdatePortrait(CharacterPortraitData character)
    {
        float energyLevel = character.energySlider.value;
        if (energyLevel >= 25f)
        {
            character.portraitImage.sprite = character.happyPortrait;
        }
        else if (energyLevel >= 12.5f)
        {
            character.portraitImage.sprite = character.happyPortrait;
        }   
        else if (energyLevel > 6.25f)
        {
            character.portraitImage.sprite = character.happyPortrait;
        }
        else if (energyLevel > 0f)
        {
            character.portraitImage.sprite = character.happyPortrait;
        }
    }
}
