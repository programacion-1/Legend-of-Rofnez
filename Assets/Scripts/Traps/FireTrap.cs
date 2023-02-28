using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using CombatEnums;

public class FireTrap : Trap
{
    private ParticleSystem particle;
    BoxCollider objCollider;

    public override void UniqueStartSettings()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        objCollider = GetComponent<BoxCollider>();
    }
    
    public override void TrapActivatedBehaviour()
    {
        SetCurrentTime(GetCurrentTime()-Time.deltaTime);
        Debug.Log(GetCurrentTime());
        if(GetCurrentTime()<=0)
        {
            particle.enableEmission = false;
            SetFireRank(1f);
            objCollider.enabled = false;
            StartCoroutine(waitToReactivate());
        }
    }

    public override void TrapDeactivatedBehaviour()
    {
        particle.enableEmission = false;
        SetFireRank(1f);
        objCollider.enabled = false;
        StopCoroutine(waitToReactivate());
    }

    public override void TrapEffect(Health target)
    {
        target.TakeDamage(GetTrapDamage(), AttackType.Special);
    }

    public override IEnumerator waitToReactivate()
    {
        yield return new WaitForSeconds(GetWaitTimeTrapDesactivater());
        objCollider.enabled = true;
        particle.enableEmission = true;
        SetFireRank(GetFireRank() + Time.deltaTime);
        Mathf.Clamp(GetFireRank(), 0, 4);
        particle.startLifetime = GetFireRank();
        SetCurrentTime(GetTime());
    }
}
