using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathActivator : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GameEvents.currentInstance.Die();
    }
}
