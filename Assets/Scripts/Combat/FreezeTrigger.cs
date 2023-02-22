using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using CombatEnums;

public class FreezeTrigger : MonoBehaviour
{
    [SerializeField] float freezeLifeSpan;
    [SerializeField] CharaType targetToFreeze;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == targetToFreeze.ToString())
        {
            Health targetHealth = other.gameObject.GetComponent<Health>();
            if(!targetHealth.CheckIfIsFreezed()) targetHealth.Freeze(freezeLifeSpan);
        } 
    }
}
