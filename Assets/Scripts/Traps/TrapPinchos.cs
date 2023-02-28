using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class TrapPinchos : Trap
{
    private Animator Anim;
    public override void UniqueStartSettings()
    {
         Anim = GetComponent<Animator>();
         SetTriggerOn();
    }
    void FixedUpdate() 
    {
  
    }
    public override void TrapActivatedBehaviour()
    {
        Anim.SetBool("IsAtacking",true);
        Anim.SetTrigger("AttackTriger");
    }
    public override void TrapDeactivatedBehaviour()
    {
        Anim.SetBool("IsAtacking",false);
    }
    public override void TrapEffect(Health target)
    {
        
    }
    public override IEnumerator waitToReactivate()
    {
        yield return new WaitForSeconds(GetWaitTimeTrapDesactivater());
        TrapActivatedBehaviour();
    }
}
