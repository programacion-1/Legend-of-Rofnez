using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class ItemInventoryMenu : MonoBehaviour
    {
        [SerializeField] Text HealthPotQuantityText;
        [SerializeField] Text MagicPotQuantityText;
        
        public Text GetHealthPotQuantityText()
        {
            return HealthPotQuantityText;
        }

        public Text GetMagicPotQuantityText()
        {
            return MagicPotQuantityText;
        }

        public void SetHealthPotQuantityText(string quantity)
        {
            HealthPotQuantityText.text = "X" + quantity;
        }

        public void SetMagicPotQuantityText(string quantity)
        {
            MagicPotQuantityText.text = "X" + quantity;
        }

        public void SetTextColor(Color newColor, Text myText)
        {
            myText.color = newColor;
        }
    }
}


