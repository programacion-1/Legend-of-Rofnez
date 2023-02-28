using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class MagicInventoryMenu : MonoBehaviour
    {
        MenuController menuController;
        [SerializeField] Sprite defaultMagicSprite;
        [SerializeField] string defaultMagicText;
        [SerializeField] Sprite currentMagicSprite;
        [SerializeField] Image currentMagicImage;
        [SerializeField] Image[] inventoryMagicSprites;
        [SerializeField] Text[] inventoryMagicTexts;
        // Start is called before the first frame update
        void Start()
        {
            menuController = GetComponent<MenuController>();
            currentMagicImage.sprite = defaultMagicSprite;
        }


        //Getters y Setters

        public Sprite GetDefaultMagicSprite()
        {
            return defaultMagicSprite;
        }
        public string GetDefaultMagicText()
        {
            return defaultMagicText;
        }

        public Sprite GetCurrentMagicSprite()
        {
            return currentMagicSprite;
        }

        public void SetCurrentMagicSprite(Sprite newSprite)
        {
            currentMagicSprite = newSprite;
        }

        public Image GetCurrentMagicImage()
        {
            return currentMagicImage;
        }

        public void SetCurrentMagicImage()
        {
            currentMagicImage.sprite = currentMagicSprite;
        }

        public Image[] GetInventoryMagicImages()
        {
            return inventoryMagicSprites;
        }

        public Text[] GetInventoryMagicTexts()
        {
            return inventoryMagicTexts;
        }

        public void SetInventoryMagicImage(int index, Sprite sprite)
        {
            inventoryMagicSprites[index].sprite = sprite;
        }

        public void SetInventoryMagicText(int index, string text)
        {
            inventoryMagicTexts[index].text = text;
        }
    }

}