using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using CombatEnums;

public class TrapB : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Vector3 impulse;
    
    
    protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null)
        {
            
            Rigidbody playerForse = other.GetComponent<Rigidbody>();
            playerForse.AddForce(impulse, ForceMode.Impulse);
            Health player = other.GetComponent<Health>();
            player.TakeDamage(20, AttackType.Special);
            Debug.Log(impulse);


        }
    }
}
