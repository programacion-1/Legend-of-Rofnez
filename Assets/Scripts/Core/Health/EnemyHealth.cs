using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;
using RPG.Combat;
using CombatEnums;

namespace RPG.Core
{
    public class EnemyHealth : Health
    {

        void Start()
        {
            ParentStartingSettings();
            SetCharaRenderer(transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>()); 
        }
        
        public override void ShowVisualChanges()
        {
            if(gameObject.name != "Boss")
            {
                ChangeRendererColor(Color.red);
                StartCoroutine(BackToNormal());  
            } 

            // renderTexture.material.color = Color.red;
            // StartCoroutine(BackToNormal());  
        }

         public override void PlayAudibleFeedback()
        {
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, impactClipSound);
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, damageClipSound);
        }


        public override void DeathBehaviour()
        {
            Deathcounter deathcounter = GameObject.FindObjectOfType<Deathcounter>();
            deathcounter.AddToCounter();
            GetComponent<Animator>().SetTrigger("Die");
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, deadClipSound);
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GameObject.FindObjectOfType<PlayerHealth>().GetComponent<MagicPoints>().RestoreMagicPoints(2.5f);
            GetComponent<RewardDrop>().CheckIfCanDropReward();
        }

        IEnumerator BackToNormal()
        {
            yield return new WaitForSeconds(0.5f);
            ChangeRendererColor(Color.white);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PlayerWeapon")
            {
                if (!CheckInvencibility())
                {
                    TakeDamage(other.transform.parent.GetComponent<AttackTrigger>().GetTriggerDamage(), AttackType.Weapon);
                    SetInvencibility(true);
                    StartCoroutine(DisableInvencibilityCo(0.5f));
                }
            }
        }
    }
}
