﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu;
        [SerializeField] GameObject fader;
        [SerializeField] GameObject weaponInventoryMenu;
        [SerializeField] GameObject currentWeaponActive;
        [SerializeField] GameObject currentMagicActive;
        [SerializeField] GameObject ammoText;
        [SerializeField] GameObject godModeText;
        [SerializeField] GameObject hpPotQuantityText;
        [SerializeField] GameObject mpPotQuantityText;
        
        public GameObject GetPauseMenu()
        {
            return pauseMenu;
        }

        public GameObject GetFader()
        {
            return fader;
        }

        public GameObject GetWeaponInventoryMenu()
        {
            return weaponInventoryMenu;
        }

        public GameObject GetCurrentWeaponActive()
        {
            return currentWeaponActive;
        }

        public GameObject GetCurrentMagicActive()
        {
            return currentMagicActive;
        }

        public GameObject GetAmmoText()
        {
            return ammoText;
        }

        public GameObject GetGodModeText()
        {
            return godModeText;
        }

        public GameObject GetHpPotionQuantityText()
        {
            return hpPotQuantityText;
        }

        public GameObject GetMpPotionQuantityText()
        {
            return mpPotQuantityText;
        }
        
        public void ShowUIObject(GameObject UIObject)
        {
            UIObject.SetActive(true);
        }

        public void HideUIObject(GameObject UIObject)
        {
            UIObject.SetActive(false);
        }
    }
}
