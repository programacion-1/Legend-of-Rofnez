using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.UI;
using CombatEnums;

namespace RPG.Combat
{
    public class Special : MonoBehaviour, IAction
    {
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        Animator anim;
        [SerializeField] string[] magicAnims;
        protected MagicPoints magicPoints;
        [SerializeField] Magic defaultMagic = null;
        [SerializeField] protected Magic currentMagic = null;

        float timeToActivateMagic = Mathf.Infinity;
        private Health specialTarget;
        MagicInventoryMenu magicInventoryMenu;

        void Start()
        {
            anim = GetComponent<Animator>();
            magicPoints = GetComponent<MagicPoints>();
            magicInventoryMenu = GameObject.FindObjectOfType<MagicInventoryMenu>();
            setCurrentMagic(defaultMagic);
            if(gameObject.tag == "Player")
            {
                
            }
        }

        protected void Update()
        {
            if(currentMagic == null) return;
            timeToActivateMagic += Time.deltaTime;    
        }

        public Magic getCurrentMagic()
        {
            return currentMagic;
        }

        public void setCurrentMagic(Magic magic)
        {
            currentMagic = magic;
            UpdateAnimatorMagicBool();
        }

        public void UpdateAnimatorMagicBool()
        {
            if(currentMagic != null) currentMagic.SetAnimatorMagicAnimation(anim, magicAnims);
        }

        public Health GetSpecialTarget()
        {
            return specialTarget;
        }
        
        public void SetSpecialTarget(Health target)
        {
            specialTarget = target;
        }


        public void SpecialAttack()
        {
            if(CheckIfCanUseMagic())
            {
                GetComponent<ActionScheduler>().StartAction(this);
                anim.ResetTrigger("StopMagicAttack");
                anim.SetTrigger("MagicAttack");
                timeToActivateMagic = 0f;
            }
            
        }
        protected bool CheckIfCanUseMagic()
        {
            bool firstCondition = currentMagic.GetMpToConsume() <= magicPoints.GetMagicPoints();
            bool secondCondition = timeToActivateMagic >= currentMagic.GetMagicCooldown();
            if(firstCondition && secondCondition) return true;
            else return false;
        }

        public virtual void InstantiateMagic()
        {
            magicPoints.ConsumeMagicPoints(currentMagic.GetMpToConsume());
            if(currentMagic.GetMagicType() == MagicType.Expansive)
            {
                GameObject areaMagic = Instantiate(currentMagic.GetEquippedPrefab(), transform.position, transform.rotation);
                areaMagic.GetComponent<AreaMagic>().SetAreaDamage(currentMagic.GetMagicDamage());
            }
            if(currentMagic.GetMagicType() == MagicType.Projectile)
            {
                GameObject projectileMagic = Instantiate(currentMagic.GetEquippedPrefab(), leftHandTransform.position, leftHandTransform.rotation);
                projectileMagic.GetComponent<Projectile>().SetTarget(specialTarget, currentMagic.GetMagicDamage());
            }
        }
        public void Cancel()
        {
            anim.ResetTrigger("MagicAttack");
            anim.SetTrigger("StopMagicAttack");
        }
    }
}
