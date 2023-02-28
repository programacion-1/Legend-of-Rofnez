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

        public void SetCurrentMagicActive(int currentMagic)
        {
            /*Image activeMagicSprite = menuController.GetCurrentMagicActive().GetComponent<Image>();
            if(currentMagic < inventoryMagicSprites.Length)
            {
                for(int i = 0; i < inventoryMagicSprites.Length; i++)
                {
                    if(i == currentMagic)
                    {

                    }
                }
            }*/
        }
    }

}