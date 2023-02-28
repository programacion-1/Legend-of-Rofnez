using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class AreaForce : MonoBehaviour
    {
        [SerializeField] float forcePower;
        [SerializeField] float forceRadius;
        [SerializeField] float forceUpwardsModifier;
        Vector3 forcePos;
        Collider[] colliders;
        [SerializeField] string target;
        [SerializeField] float recoveryTimer;
        public bool canExplode;
        List<Rigidbody> rbs= new List<Rigidbody>();
        
        // Start is called before the first frame update
        void Start()
        {
            canExplode = true;
        }

        public void SetTarget(string t)
        {
            target = t;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.tag == target)
            {
                if(canExplode) ApplyForce();
            }
        }

        public void ApplyForce()
        {
            forcePos = transform.position;
            colliders = Physics.OverlapSphere(forcePos, forceRadius);
            canExplode = false;
            StartCoroutine("forceTimeRecovery");
        }

        private IEnumerator forceTimeRecovery()
        {
            foreach(Collider hit in colliders)
            {
                if(hit.tag == target && !hit.GetComponent<Health>().isBoss)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    if (rb != null && rb.GetComponent<Health>().GetHP() > rb.GetComponent<Health>().GetMaxHP() / 3)
                    {
                        rb.isKinematic = false;
                        rb.useGravity = true;
                        rb.AddExplosionForce(forcePower, forcePos, forceRadius, forceUpwardsModifier, ForceMode.VelocityChange);
                        hit.GetComponent<ActionScheduler>().CancelCurrentAction();
                        rbs.Add(rb);
                    }     
                }
            }
             yield return new WaitForSeconds(recoveryTimer);
             EnableForce();
        }

        void OnDisable()
        {
           EnableForce();
        }

        public void EnableForce()
        {
            if(rbs.Count > 0)
            {
                foreach(Rigidbody rB in rbs)
                {
                rB.isKinematic = true;
                rB.useGravity = false;
                }
                canExplode = true;
                rbs.Clear();
            }
        }
        
    }
}
