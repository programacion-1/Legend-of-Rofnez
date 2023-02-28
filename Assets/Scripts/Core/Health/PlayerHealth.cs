using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;
using RPG.Combat;
using CombatEnums;

namespace RPG.Core
{
    public class PlayerHealth : Health
    {
        HealBar bar;
        [SerializeField] string healthBarName;
        PlayerMinMaxQuantityText healthText;
        [SerializeField] string healthBarTextName;

        public void SetStartingHealthSettings()
        {
            ParentStartingSettings();
            HealBar[] healbars = GameObject.FindObjectsOfType<HealBar>();
            for (int i = 0; i < healbars.Length; i++)
            {
                if (healbars[i].gameObject.name == healthBarName)
                {
                    bar = healbars[i];
                    break;
                }
            }
            PlayerMinMaxQuantityText[] playerMinMaxQuantityTexts = GameObject.FindObjectsOfType<PlayerMinMaxQuantityText>();
            for (int i = 0; i < playerMinMaxQuantityTexts.Length; i++)
            {
                if (playerMinMaxQuantityTexts[i].gameObject.name == healthBarTextName)
                {
                    healthText = playerMinMaxQuantityTexts[i];
                    break;
                }
            }
            ShowVisualChanges();
        }

        public override void ShowVisualChanges()
        {
            bar.ChangeBarFiller(GetHP(), GetMaxHP());
            healthText.SetQuantityText(GetHP(), GetMaxHP());
        }

        public override void DeathBehaviour()
        {
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, deadClipSound);
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public override void PlayAudibleFeedback()
        {
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, impactClipSound);
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, damageClipSound);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "EnemyWeapon")
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
