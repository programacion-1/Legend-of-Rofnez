﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.UI;
using CombatEnums;

namespace RPG.Core
{
    public class SiegeTowerBossHealth : Health
    {
        [Header("Boss Variables")]
        [SerializeField] GameObject explosionVFX;
        [SerializeField] float hpAmountToActivateShield;
        float currentAmount = 0;
        int timesShieldWasActivated = 0;
        [SerializeField] int[] timesToChangeIndexes;
        [Header("Health Bar")]
        [SerializeField] HealBar bar;

        [Header("Boss Audio Clips")]
        AudioManager bossAudioManager;
        [SerializeField] AudioClip swearClip;


        void Start()
        {
            ParentStartingSettings();
            // SetHealthBar();
            bossAudioManager = GameObject.FindObjectOfType<AudioManager>();
        }

        public void SetHealthBar()
        {
            HealBar[] healbars = GameObject.FindObjectsOfType<HealBar>();
            for (int i = 0; i < healbars.Length; i++)
            {
                if (healbars[i].gameObject.name == "BossHealthBar")
                {
                    bar = healbars[i];
                    break;
                }
            }
            ShowVisualChanges();
        }

        public override void DeathBehaviour()
        {
            StartCoroutine(ExplodeCo());
        }
        public override void ShowVisualChanges()
        {
            bar.ChangeBarFiller(GetHP(), GetMaxHP());
            ChangeHPAmountToActivateShield();
        }

        public override void PlayAudibleFeedback()
        {
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, impactClipSound);
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, damageClipSound);
        }

        private void ChangeHPAmountToActivateShield()
        {
            currentAmount += GetMaxHP() - GetHP() - currentAmount - hpAmountToActivateShield * timesShieldWasActivated;
        }

        public bool CheckIfICanActivateShield()
        {
            if (currentAmount >= hpAmountToActivateShield)
            {
                timesShieldWasActivated++;
                audioManager.TryToPlayClip(audioManager.EnemySFXSources, swearClip);
                currentAmount = 0;
                return true;
            }
            else
            {
                return false;
            }
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

        private IEnumerator ExplodeCo()
        {
            float explosionYPos = transform.position.y + 1.75f;
            Vector3 explosionPos = new Vector3(transform.position.x, explosionYPos, transform.position.z);
            yield return new WaitForSeconds(5f);
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, deadClipSound);
            Instantiate(explosionVFX, explosionPos, transform.rotation);
            yield return new WaitForSeconds(0.25f);
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        public int SetIndex()
        {
            int index = 0;
            for(int i = 0; i < timesToChangeIndexes.Length; i++)
            {
                if(timesShieldWasActivated <= timesToChangeIndexes[i]) break;
                index++;
            }
            print(index);
            return index;
        }

    }
}
