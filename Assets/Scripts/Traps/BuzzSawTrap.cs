using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class BuzzSawTrap : Trap
{
    [SerializeField] private int damage;
    [SerializeField] private Vector3 impulse;
    Animator anim;
    
    public override void UniqueStartSettings()
    {
        anim = GetComponent<Animator>();
    }
    
    public override void TrapActivatedBehaviour()
    {
        /*SetCurrentTime(GetCurrentTime()-Time.deltaTime);
        Debug.Log(GetCurrentTime());
        if(GetCurrentTime()<=0)
        {
            particle.enableEmission = false;
            SetFireRank(1f);
            objCollider.enabled = false;
            StartCoroutine(waitToReactivate());
        }*/
    }

    public override void TrapDeactivatedBehaviour()
    {
        /*particle.enableEmission = false;
        SetFireRank(1f);
        objCollider.enabled = false;
        StopCoroutine(waitToReactivate());*/
    }

    public override void TrapEffect(Health target)
    {
        //target.TakeDamage(GetTrapDamage());
    }

    public override IEnumerator waitToReactivate()
    {
        /*yield return new WaitForSeconds(GetWaitTimeTrapDesactivater());
        objCollider.enabled = true;
        particle.enableEmission = true;
        SetFireRank(GetFireRank() + Time.deltaTime);
        Mathf.Clamp(GetFireRank(), 0, 4);
        particle.startLifetime = GetFireRank();
        SetCurrentTime(GetTime());*/
    }
}
