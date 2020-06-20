using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    //Isak and Danny
    //Activates event when entering a trigger collider.

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
