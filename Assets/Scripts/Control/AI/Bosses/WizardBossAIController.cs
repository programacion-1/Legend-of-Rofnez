using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Control
{
    public class WizardBossAIController : AIController
    {
        WizardBossHealth wizardBossHealth;
        [Header("Tornado Variables")]
        [SerializeField] GameObject tornadoVFX;
        [SerializeField] GameObject tornadoTrigger;
        bool canDoATornado;
        bool madeATornado;
        Animator animator;
        [Header("Boss Movement Variables")]
        [SerializeField] PatrolPath bossWaypoints;
        Vector3 currentPosition;
        Transform lastWaypoint;
        int currentWaypointIndex;
        [SerializeField] float waypointTolerance = 0.5f;
        [SerializeField] GameObject bossCharacter;
        [SerializeField] float teleportTimer;
        float timeSinceTeleport = Mathf.Infinity;
        bool canTeleport;
        [SerializeField] GameObject teleportVFX;
        [SerializeField] float teleportVFXPosY;
        float teleportVFXDuration;
        [Header("Audio Clips")]
        AudioManager audioManager;
        [SerializeField] AudioClip teleportClip;
        [SerializeField] AudioClip tornadoClip;
        void Start()
        {
            ParentStartingSettings();
            tornadoTrigger.SetActive(false);
            wizardBossHealth = GetComponent<WizardBossHealth>();
            animator = GetComponent<Animator>();
            lastWaypoint = bossWaypoints.transform.GetChild(bossWaypoints.transform.childCount-1);
            currentPosition = transform.position;
            teleportVFXDuration = teleportVFX.GetComponent<ParticleSystem>().duration;
            audioManager = GameObject.FindObjectOfType<AudioManager>();
        }
        
        public override void UpdateTimers()
        {
            timeSinceTeleport += Time.deltaTime;
        }
        public override void UpdateBehaviour()
        {
            if(InAttackRangeOfPlayer())
            {
                canDoATornado = wizardBossHealth.CheckIfICanDoATornadoMovement();
                if(canDoATornado)
                {
                    if(!madeATornado)TornadoBehaviour();
                }
                else
                {
                    if(timeSinceTeleport >= teleportTimer && !madeATornado) TeleportBehaviour();
                    if(AtWaypoint())
                    {
                        if(canTeleport) StartCoroutine(Appear());
                        else if(bossCharacter.activeInHierarchy) AttackBehaviour();
                    }
                }
            }
                // canDoATornado = wizardBossHealth.CheckIfICanDoATornadoMovement();
                // if(canDoATornado && !canTeleport)
                // {
                //     if(!madeATornado)TornadoBehaviour();
                // }
                // else
                // {
                //     if(timeSinceTeleport >= teleportTimer) TeleportBehaviour();
                //     if(AtWaypoint())
                //     {
                //         if(canTeleport) StartCoroutine(Appear());
                //         else if(bossCharacter.activeInHierarchy) AttackBehaviour();
                //     }
                // }
        }

        private void TeleportBehaviour() // Comportamiento a la hora de moverse
        {
            GetPlayer().GetComponent<ActionScheduler>().StartAction(null); //Cancelo la acción del player para que, en el caso de que esté atacando, no persiga al boss, para evitar que el jugador sepa cual será la próxima posición del boss
            GetComponent<ActionScheduler>().StartAction(null); //Cancelo mi acción, por si el boss está atacando o por atacar
            timeSinceTeleport = 0;
            canTeleport = true;
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, teleportClip);
            GetHealth().SpawnShader(teleportVFX);
            GameObject.Find(teleportVFX.name+"(Clone)").transform.position = new Vector3(transform.position.x, transform.position.y+teleportVFXPosY, transform.position.z);
            GetHealth().SetInvencibility(true);
            currentWaypointIndex = GetNextTeleportWaypoint(); //Asigno nueva posición para teletransportarme
            StartCoroutine(Vanish(GetCurrentWaypoint()));
        }

        private IEnumerator Vanish(Vector3 nextPosition) // Corrutina que hace desaparecer el modelo del boss
        {
            yield return new WaitForSeconds(teleportVFXDuration / 4);
            bossCharacter.SetActive(false); //Al cuarto de duración de la animación de la partícula, desactivo el modelo del jefe
            yield return new WaitForSeconds(teleportVFXDuration);
            GetMover().StartMoveAction(nextPosition); // Al pasar la duración de la animación, el boss se mueve hacia el próximo waypoint
        }

        private IEnumerator Appear()
        {
            canTeleport = false;
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, teleportClip);
            GetHealth().SpawnShader(teleportVFX);
            GameObject.Find(teleportVFX.name+"(Clone)").transform.position = new Vector3(transform.position.x, transform.position.y + teleportVFXPosY, transform.position.z);
            yield return new WaitForSeconds(teleportVFXDuration);
            GetHealth().SetInvencibility(false);
            bossCharacter.SetActive(true);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private Vector3 GetCurrentWaypoint()
        {
            return bossWaypoints.GetWaypoint(currentWaypointIndex);
        }

        private int GetNextTeleportWaypoint()
        {
            int nextWaypoint = currentWaypointIndex;
            while(nextWaypoint == currentWaypointIndex) nextWaypoint = Random.Range(0, bossWaypoints.transform.childCount-1);
            return nextWaypoint;
        }

        private void TornadoBehaviour() //Ataque de Tornado
        {
            audioManager.TryToPlayClip(audioManager.EnemySFXSources, tornadoClip);
            GetComponent<ActionScheduler>().StartAction(null);
            madeATornado = true;
            GetHealth().SetInvencibility(true);
            animator.SetBool("isKicking", true);
            StartCoroutine(StopTornado());
        }
        private IEnumerator StopTornado() // Corrutina para parar el ataque de tornado
        {
            yield return new WaitForSeconds(0.5f);
            GetHealth().SpawnShader(tornadoVFX);
            yield return new WaitForSeconds(0.5f);
            tornadoTrigger.SetActive(true);
            yield return new WaitForSeconds(3f);
            madeATornado = false;
            ParticleSystem tornado = GameObject.Find(tornadoVFX.name+"(Clone)").GetComponent<ParticleSystem>();
            tornado.loop = false;
            animator.SetBool("isKicking", false);
            yield return new WaitForSeconds(tornado.duration);
            tornadoTrigger.SetActive(false);
            GetHealth().SetInvencibility(false);
            Destroy(tornado.gameObject);
        }
        public override void AttackBehaviour()
        {
            GetFighter().Attack(GetPlayer());
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, GetChaseDistance());
        }
    }

}