﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using CombatEnums;

public class Explosion : MonoBehaviour
{
    [SerializeField] float explosionDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(explosionDamage, AttackType.Special);
        }
    }
}
