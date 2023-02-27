using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatEnums;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.UI;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Fighter fighter;
        Special special;
        Health health;
        MagicPoints magicPoints;
        WeaponInventory weaponInventory;
        ItemInventory itemInventory;
        MenuController menuController;
        PlayerCursor playerCursor;
        bool GodMode;
        bool canHealHP = true;
        bool canHealMP = true;
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            special = GetComponent<Special>();
            health = GetComponent<Health>();
            magicPoints = GetComponent<MagicPoints>();
            weaponInventory = GetComponent<WeaponInventory>();
            itemInventory = GetComponent<ItemInventory>();
            playerCursor = GetComponent<PlayerCursor>();
            menuController = GameObject.FindObjectOfType<MenuController>();
        }
        void Update()
        {
            if(health.IsDead()) return;
            if(Input.GetKeyDown(KeyCode.F))
            {
                if(GodMode) GodMode = false;
                else GodMode = true;
            }
            if(GodMode)
            {
                health.SetInvencibility(true);
                menuController.ShowUIObject(menuController.GetGodModeText());
            } 
            else
            {
                health.SetInvencibility(false);
                menuController.HideUIObject(menuController.GetGodModeText());
            }
            //Test Area Begin
            if(Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("test");           
            }
            //Test Area End 
            if(ActivateSpecialAttack()) return;
            if(InteractWithCombat()) return;
            
            if(Input.GetKeyDown(KeyCode.Q))
            {
                ChangePlayerActiveWeapon();
                return;
            }

            if(Input.GetKeyDown(KeyCode.Tab))
            {
                menuController.ShowUIObject(GameObject.FindObjectOfType<MenuController>().GetWeaponInventoryMenu());
                GameObject.FindObjectOfType<WeaponInventorMenu>().SetAmmoInventoryText();
                return;
            }
            if(Input.GetKeyUp(KeyCode.Tab))
            {
                menuController.HideUIObject(GameObject.FindObjectOfType<MenuController>().GetWeaponInventoryMenu());
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                 if(itemInventory.GetCurrentHPpotions() > 0 && canHealHP) StartCoroutine("healHP");
            }

            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                 if(itemInventory.GetCurrentMPpotions() > 0 && canHealMP) StartCoroutine("healMP");
            }

            if(InteractWithMovement()) return;
        }

        private bool ActivateSpecialAttack()
        {
            if(Input.GetMouseButtonDown(1) && special.getCurrentMagic() != null)
            {
                bool canDoSpecial = true;

                if(special.getCurrentMagic().GetMagicType() == MagicType.Projectile)
                {
                    if(GetProjectileTarget() != null)
                    {
                        special.SetSpecialTarget(GetProjectileTarget());
                        canDoSpecial = true;
                    }
                    else canDoSpecial = false;
                }
                
                if(canDoSpecial)
                {
                    special.SpecialAttack();
                    return true;
                }

                else return false;
            }
            else return false;
        }
        
        // Busco con raycast si encuentro un objetivo para pelear, chequeo si puedo atacarlo y si hago click, lo ataco
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                playerCursor.SetFightCursor();
                if(target == null) continue;
                if(!fighter.CanAttack(target.gameObject)) continue;
                if(Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target.gameObject);
                } 
                return true;
            }
            playerCursor.SetDefaultCursor();
            return false;
        }

        //Chequeo con Raycast algún punto en el mundo en donde pueda hacer moverme y si hago click y tengo lugar, me muevo
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit && !CheckRaycastTags(hit))
            {
                playerCursor.SetMoveCursor();
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            playerCursor.SetDefaultCursor();
            return false;
        }

        //Devuelve el punto donde esté apuntando con el mouse en la posición del mundo
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void ChangePlayerActiveWeapon()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
            WeaponInventorMenu myMenu = GameObject.FindObjectOfType<WeaponInventorMenu>();
            if (weaponInventory.GetActiveWeapon() == weaponInventory.GetMeleeWeapon() && weaponInventory.GetRangedWeapon() != null)
            {
                weaponInventory.SetActiveWeapon(weaponInventory.GetRangedWeapon());
                myMenu.SetCurrentWeaponActive(1);
            }
            else if (weaponInventory.GetActiveWeapon() == weaponInventory.GetRangedWeapon() && weaponInventory.GetMeleeWeapon() != null)
            {
                weaponInventory.SetActiveWeapon(weaponInventory.GetMeleeWeapon());
                myMenu.SetCurrentWeaponActive(0);
            }
            fighter.EquipWeapon(weaponInventory.GetActiveWeapon());

            if(fighter.GetCurrentShield() != null)
            {
                if (fighter.GetCurrentWeapon() == weaponInventory.GetMeleeWeapon()) fighter.ShowShield();
                else fighter.HideShield();
            }
        }

        private bool CheckRaycastTags(RaycastHit hitPoint)
        {
            switch(hitPoint.collider.gameObject.tag)
            {
                case "Prop":
                    return true;
                default:
                    return false;
            }
        }

        private Health GetProjectileTarget()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                if(target == null) continue;
                else
                {
                    //Cambio la rotación del personaje para que apunte al objetivo. #SUPERHARDCODEADOXD
                    transform.rotation = target.transform.rotation * Quaternion.Euler(0, -target.transform.rotation.y*180,0);
                    return target.GetComponent<Health>();
                } 
            }
            return null;
        }

        private IEnumerator healHP()
        {
            canHealHP = false;
            itemInventory.HealHP(health);
            GetComponent<ActionScheduler>().CancelCurrentAction();
            yield return new WaitForSeconds(1f);
            canHealHP = true;
        }

        private IEnumerator healMP()
        {
            canHealMP = false;
            itemInventory.HealMP(magicPoints);
            GetComponent<ActionScheduler>().CancelCurrentAction();
            yield return new WaitForSeconds(1f);
            canHealMP = true;
        }
    }
}