using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Core
{
    public class MagicInventory : MonoBehaviour
    {
        
        [SerializeField] List<Magic> myMagics = new List<Magic>();
        Special special;
        // Start is called before the first frame update
        void Start()
        {
            special = GetComponent<Special>();
            AddNewMagicToInventory(special.getCurrentMagic());
        }


        public void AddNewMagicToInventory(Magic newMagic)
        {
            myMagics.Add(newMagic);
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
    }

}
