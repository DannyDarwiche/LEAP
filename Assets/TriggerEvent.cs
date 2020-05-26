using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField]
    int id;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameEvents.currentInstance.TriggerOn(id);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            GameEvents.currentInstance.TriggerOff(id);
    }
}
