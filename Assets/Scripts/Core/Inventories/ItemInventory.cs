using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class ItemInventory : MonoBehaviour
    {
        //HP Potions
        [SerializeField] float currentHP_potions;
        [SerializeField] float maxHP_potions;
        [SerializeField] float pointsToHealHP;
        [SerializeField] GameObject healVFX;

        //MP Potions
        [SerializeField] float currentMP_potions;
        [SerializeField] float maxMP_potions;
        [SerializeField] float pointsToHealMP;
        [SerializeField] GameObject mpVFX;


        //Getters y Setters

        public float GetCurrentHPpotions()
        {
            return currentHP_potions;
        }

        public float GetMaxHPpotions()
        {
            return maxHP_potions;
        }

        public float GetCurrentMPpotions()
        {
            return currentMP_potions;
        }

        public float GetMaxMPpotions()
        {
            return maxMP_potions;
        }

        public void SetCurrentHPpotions(float potions)
        {
            currentHP_potions = potions;
        }

        public void SetMaxHPpotions(float potions)
        {
            maxHP_potions = potions;
        }

        public void SetCurrentMPpotions(float potions)
        {
            currentMP_potions = potions;
        }

        public void SetMaxMPpotions(float potions)
        {
            maxMP_potions = potions;
        }

        public void HealHP(Health health)
        {
            SetCurrentHPpotions(currentHP_potions-1);
            health.SpawnShader(healVFX);
            health.Heal(pointsToHealHP);
        }

        public void HealMP(MagicPoints magicPoints)
        {
            SetCurrentMPpotions(currentMP_potions-1);
            magicPoints.GetComponent<Health>().SpawnShader(mpVFX);
            magicPoints.RestoreMagicPoints(pointsToHealMP);
        }


    }

}
