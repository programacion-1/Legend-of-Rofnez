using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class ItemInventory : MonoBehaviour
    {
        //HP Potions
        [SerializeField] int currentHP_potions;
        [SerializeField] int maxHP_potions;
        [SerializeField] float pointsToHealHP;
        [SerializeField] GameObject healVFX;

        //MP Potions
        [SerializeField] int currentMP_potions;
        [SerializeField] int maxMP_potions;
        [SerializeField] float pointsToHealMP;
        [SerializeField] GameObject mpVFX;

        ItemInventoryMenu itemInventoryMenu;

        void Start()
        {
            itemInventoryMenu = GameObject.FindObjectOfType<ItemInventoryMenu>().GetComponent<ItemInventoryMenu>();
            SetHPPotQuantity();
            SetMPPotQuantity();
        }


        //Getters y Setters

        public int GetCurrentHPpotions()
        {
            return currentHP_potions;
        }

        public int GetMaxHPpotions()
        {
            return maxHP_potions;
        }

        public int GetCurrentMPpotions()
        {
            return currentMP_potions;
        }

        public int GetMaxMPpotions()
        {
            return maxMP_potions;
        }

        public void SetCurrentHPpotions(int potions)
        {
            currentHP_potions = potions;
        }

        public void SetMaxHPpotions(int potions)
        {
            maxHP_potions = potions;
        }

        public void SetCurrentMPpotions(int potions)
        {
            currentMP_potions = potions;
        }

        public void SetMaxMPpotions(int potions)
        {
            maxMP_potions = potions;
        }

        public void HealHP(Health health)
        {
            SetCurrentHPpotions(currentHP_potions-1);
            health.SpawnShader(healVFX);
            health.Heal(pointsToHealHP);
            SetHPPotQuantity();
        }

        public void HealMP(MagicPoints magicPoints)
        {
            SetCurrentMPpotions(currentMP_potions-1);
            magicPoints.GetComponent<Health>().SpawnShader(mpVFX);
            magicPoints.RestoreMagicPoints(pointsToHealMP);
            SetMPPotQuantity();
        }

        public void SetHPPotQuantity()
        {
            itemInventoryMenu.SetHealthPotQuantityText(currentHP_potions.ToString());
            if(currentHP_potions == maxHP_potions) itemInventoryMenu.SetTextColor(Color.green, itemInventoryMenu.GetHealthPotQuantityText());
            else if(currentHP_potions == 0) itemInventoryMenu.SetTextColor(Color.red, itemInventoryMenu.GetHealthPotQuantityText());
            else itemInventoryMenu.SetTextColor(Color.white, itemInventoryMenu.GetHealthPotQuantityText());
        }

        public void SetMPPotQuantity()
        {
            itemInventoryMenu.SetMagicPotQuantityText(currentMP_potions.ToString());
            if(currentMP_potions == maxMP_potions) itemInventoryMenu.SetTextColor(Color.green, itemInventoryMenu.GetMagicPotQuantityText());
            else if(currentMP_potions == 0) itemInventoryMenu.SetTextColor(Color.red, itemInventoryMenu.GetMagicPotQuantityText());
            else itemInventoryMenu.SetTextColor(Color.white, itemInventoryMenu.GetMagicPotQuantityText());
        }
    }

}
