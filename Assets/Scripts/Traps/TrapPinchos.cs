using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using CombatEnums;

public class TrapPinchos : Trap
{
    private Animator Anim;
    [SerializeField] string animIsAttackingBoolVar;
    [SerializeField] string animAttackTriggerVar;
    [SerializeField] string animIsEnabledBoolVar;
    bool isActive;
    public override void UniqueStartSettings()
    {
         Anim = GetComponent<Animator>();
         Anim.SetBool(animIsEnabledBoolVar,true);
         isActive = false;
    }

    public override void TrapActivatedBehaviour()
    {
        if(!isActive) StartCoroutine("waitToReactivate");
    }
    public override void TrapDeactivatedBehaviour()
    {
        Anim.SetBool(animIsEnabledBoolVar,false);
    }
    public override void TrapEffect(Health target)
    {
        target.TakeDamage(GetTrapDamage(), AttackType.Special);
    }
    public override IEnumerator waitToReactivate()
    {
        isActive = true;
        Anim.SetBool(animIsAttackingBoolVar,true);
        Anim.SetTrigger(animAttackTriggerVar);
        yield return new WaitForSeconds(GetWaitTimeTrapDesactivater());
        Anim.SetBool(animIsAttackingBoolVar,false);
        Anim.ResetTrigger(animAttackTriggerVar);
        isActive = false;
    }
}
