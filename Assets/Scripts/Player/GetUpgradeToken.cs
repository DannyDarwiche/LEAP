using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgradeToken : MonoBehaviour
{
    //Isak and Danny
    //When the player enters the trigger an upgrade token is collected and the token orb destroyed.

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.currentInstance.TokenGet();
            Destroy(gameObject);
        }
    }
}
