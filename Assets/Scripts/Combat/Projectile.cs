﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class Projectile : MonoBehaviour
{
    [SerializeField] Health target;
    [SerializeField] float speed;
    [SerializeField] private bool isHoming;
    float damage;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] float lifeSpan = 1f;
    public AudioSource impactSound;
    float lifeTime = 0f;
    
    private void Start()
    {
        transform.LookAt(GetAimLocation());
        StartCoroutine(DestroyProjectileByLifeSpan());
    }

    public void SetTarget(Health t, float d)
    {
        target = t;
        damage = d;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null) return;
        if(isHoming && !target.IsDead()) transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule == null) return target.transform.position;
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool canBeDestroyed = false;
        if(other.gameObject == target.gameObject)
        {
            if(!target.IsDead() && !target.CheckInvencibility()) target.TakeDamage(damage);
            canBeDestroyed = ImpactEffect();
        }
        else if(other.gameObject.tag == "Prop" || other.gameObject.tag == "DestroyableObstacle")
        {
            canBeDestroyed = ImpactEffect();
        }
        if(canBeDestroyed)
        {
            StartCoroutine(DestroyProjectileByImpact());
        }
    }

    private bool ImpactEffect()
    {
        impactSound.Play();
        if(hitEffect != null)
        {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
        }
        return true;
    }  

    private IEnumerator DestroyProjectileByLifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }

    private IEnumerator DestroyProjectileByImpact()
    {
        speed = 0;
        StopCoroutine(DestroyProjectileByLifeSpan());
        yield return new WaitForSeconds(impactSound.clip.length);
        Destroy(gameObject);
        
    }
}
