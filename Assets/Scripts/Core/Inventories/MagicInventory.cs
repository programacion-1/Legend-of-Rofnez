using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Combat;
using RPG.UI;

namespace RPG.Core
{
    public class MagicInventory : MonoBehaviour
    {
        
        [SerializeField] List<Magic> myMagics = new List<Magic>();
        Special special;
        MagicInventoryMenu magicInventoryMenu;
        // Start is called before the first frame update
        void Start()
        {
            special = GetComponent<Special>();
            magicInventoryMenu = GameObject.FindObjectOfType<MagicInventoryMenu>();
            AddNewMagicToInventory(special.getCurrentMagic());
        }


        public void AddNewMagicToInventory(Magic newMagic)
        {
            myMagics.Add(newMagic);
            UpdateMagicInventoryMenu();
        }

        public List<Magic> GetMagicList()
        {
            return myMagics;
        }

        public Magic GetActiveMagic()
        {
            for(int i = 0; i < myMagics.Count; i++)
            {
                if(myMagics[i] == special.getCurrentMagic()) return myMagics[i];
            }
            return null;
        }

        public void SetActiveMagic(int mag)
        {
            if(mag >= myMagics.Count) mag = 0;
            special.setCurrentMagic(myMagics[mag]);
        }

        public int MagicQuantity()
        {
            return myMagics.Count;
        }

        public void UpdateMagicInventoryMenu()
        {
            for(int i = 0; i < magicInventoryMenu.GetInventoryMagicImages().Length; i++)
            {
                Sprite spriteToUpdate = magicInventoryMenu.GetDefaultMagicSprite();
                string textToUpdate = magicInventoryMenu.GetDefaultMagicText();
                if(i < GetMagicList().Count)
                {
                    spriteToUpdate = GetMagicList()[i].GetMagicSprite();
                    textToUpdate = GetMagicList()[i].name;
                }
                magicInventoryMenu.SetInventoryMagicImage(i,spriteToUpdate);
                magicInventoryMenu.SetInventoryMagicText(i,textToUpdate);
            }
        }
    }

}
