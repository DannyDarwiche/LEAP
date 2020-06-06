using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    //Isak and Danny
    //When the player enters the trigger an upgrade is collected and the orb destroyed.

    [SerializeField]
    AbilityType abilityType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.currentInstance.UpgradeGet(abilityType);
            Destroy(gameObject);
        }
    }
}
