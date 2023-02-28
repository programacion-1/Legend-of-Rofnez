using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class PlayerMinMaxQuantityText : MonoBehaviour
    {
        [SerializeField] Text quantityText;

        public void SetQuantityText(float currentQuantity, float maxQuantity)
        {
            quantityText.text = Mathf.Round(currentQuantity).ToString() + "/" + maxQuantity.ToString();
        }
    }

}