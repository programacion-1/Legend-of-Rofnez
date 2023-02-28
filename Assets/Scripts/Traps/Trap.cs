using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public abstract class Trap : MonoBehaviour
{
    private float currentTime;
    [SerializeField] float time = 2f;
    private float fireRank = 1;
    [SerializeField] float waitTimeTrapDesactivater = 5f; 
    [SerializeField] private float trapDamage = 10f; 

    //Trigger para saber si la trampa está activa o no
    [SerializeField] bool onOffTrigger;
    
    private void Start()
    {
        currentTime = 0;
        SetTriggerOn();
        UniqueStartSettings();
    }

    //Getters y Setters

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public void SetCurrentTime(float t)
    {
        currentTime = t;
    }

    public float GetTime()
    {
        return time;
    }

    public float GetFireRank()
    {
        return fireRank;
    }

    public void SetFireRank(float fR)
    {
        fireRank = fR;
    }

    public float GetWaitTimeTrapDesactivater()
    {
        return waitTimeTrapDesactivater;
    }

    public float GetTrapDamage()
    {
        return trapDamage;
    }

    public void SetTriggerOn()
    {
        onOffTrigger = true;
    }

    public void SetTriggerOff()
    {
        onOffTrigger = false;
    }

    //FixedUpdate

    private void FixedUpdate()
    {
        if(onOffTrigger)
        {
            TrapActivatedBehaviour();
        }
        else
        {
            TrapDeactivatedBehaviour();
        }       
    }

    //Abstract Classes

    public abstract IEnumerator waitToReactivate();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TrapEffect(other.GetComponent<Health>());
        }
    }

    public abstract void UniqueStartSettings();

    public abstract void TrapActivatedBehaviour();

    public abstract void TrapDeactivatedBehaviour();

    public abstract void TrapEffect(Health target);

}
