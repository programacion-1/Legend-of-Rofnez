using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;

namespace RPG.Core
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        [SerializeField] float maxHealthPoints = 100f;
        private bool isDead;
        public bool poisoned;
        public bool isFreezed;
        float freezeLifeSpan;
        public int damagetik;
        public float poisonDamage;
        [SerializeField] Renderer charaRenderer;
        [SerializeField] Transform shaderSpawnpoint;
        [SerializeField] Color freezeColor;
        bool isInvencible;

        [Header("Audio Clips")]
        public AudioManager audioManager;
        public AudioClip deadClipSound;
        public AudioClip damageClipSound;
        public AudioClip impactClipSound;
        public AudioClip poisonClipSound;
        public AudioClip shieldClipSound;
    

        public void ParentStartingSettings()
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
            healthPoints = maxHealthPoints;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void SetCharaRenderer(Renderer renderer)
        {
            charaRenderer = renderer;
        }

        public Renderer GetCharaRenderer(Renderer renderer)
        {
            return charaRenderer;
        }

        public void ChangeRendererColor(Color color)
        {
            charaRenderer.material.color = color;
        }

        public void TakeDamage(float damage)
        {
            //Antes de recibir daño, chequeo si puedo deflectar el daño en el caso de tener un escudo equipado
            if(!isDead && !isInvencible)
            { 
                if (GetComponent<Fighter>() == null)
                {
                    DamageBehavoiur(damage);
                }
                else
                {
                    if(damage > 0)
                    {
                        if (GetComponent<Fighter>().GetCurrentShield() != null)
                        {
                            if (!GetComponent<Fighter>().GetCurrentShield().DeflectDamage())
                            {
                                DamageBehavoiur(damage);
                            }
                            else
                            {
                                audioManager.TryToPlayClip(audioManager.PlayerSFXSources, shieldClipSound);
                            }
                        }
                        else DamageBehavoiur(damage);
                    }                    
                }
            }
        }

        private void DamageBehavoiur(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            ShowVisualChanges();
            PlayAudibleFeedback();
            if (healthPoints == 0)
            {
                Die();
            }
        }

        public void Freeze(float secondsToDefreeze)
        {
            isFreezed = true;
            ChangeRendererColor(freezeColor);
            GetComponent<Animator>().speed = 0;
            freezeLifeSpan = secondsToDefreeze;
            StartCoroutine("Defreeze");
        }

        IEnumerator Defreeze()
        {
            yield return new WaitForSeconds(freezeLifeSpan);
            isFreezed = false;
            GetComponent<Animator>().speed = 1;
            ChangeRendererColor(Color.white);
        }

        public void StopFreezing()
        {
            StopCoroutine("Freeze");
            isFreezed = false;
            GetComponent<Animator>().speed = 1;
            ChangeRendererColor(Color.white);
        }

        public bool CheckIfIsFreezed()
        {
            return isFreezed;
        }

        public void poisonDamages(int tik,float damage) // Función que setea el daño y el tik del veneno y ejecuta la corrutina tikDamage()
        {
            audioManager.TryToPlayClip(audioManager.PlayerSFXSources, poisonClipSound);
            poisonDamage = damage;
            damagetik = tik;
            poisoned = true;
            ChangeRendererColor(Color.green);
            StartCoroutine("tikDamage");
        }
        IEnumerator tikDamage() //Corrutina que restará daño al personaje por una cantidad igual al valor de 'damageTik'
        {
            for(int i = 0; i < damagetik; i++)
            {
                yield return new WaitForSeconds(1f);
                TakeDamage(poisonDamage);

            }
            poisoned = false;
            ChangeRendererColor(Color.white);
        }

        public abstract void ShowVisualChanges();
        public abstract void PlayAudibleFeedback();
        public void Heal(float healPoints)
        {
            healthPoints = Mathf.Min(healthPoints + healPoints, maxHealthPoints);
            ShowVisualChanges();
        }

        public float GetHP()
        {
            return healthPoints;
        }
        public float GetMaxHP()
        {
            return maxHealthPoints;
        }
        public void Die()
        {
            if (isDead) return;
            isDead = true;
            if(isFreezed) StopFreezing();
            DeathBehaviour();
        }

        public abstract void DeathBehaviour();

        public void SpawnShader(GameObject shader) // Función para spawnear un efecto visual
        {
            GameObject newShader = GameObject.Instantiate(shader);
            newShader.transform.parent = shaderSpawnpoint;
            newShader.transform.position = shaderSpawnpoint.position;
            // newShader.transform.rotation = shaderSpawnpoint.rotation;
        }

        // Función para la invensibilidad

        public bool CheckInvencibility()
        {
            return isInvencible;
        }

        public void SetInvencibility(bool invencibility)
        {
            isInvencible = invencibility;
        }

        public IEnumerator DisableInvencibilityCo(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            SetInvencibility(false);
        }
    }
}