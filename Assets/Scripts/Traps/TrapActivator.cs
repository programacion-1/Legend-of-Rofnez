using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    [SerializeField] private Trap[] trapsToDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for(int i = 0; i<trapsToDeactivate.Length; i++)
            {
                trapsToDeactivate[i].SetTriggerOn();
            }
        }
    }
}
