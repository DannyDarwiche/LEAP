using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancingPuzzleZone : MonoBehaviour
{
 
    [SerializeField]
    balancingBarrel balacningBarrel;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            balacningBarrel.started = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            balacningBarrel.started = false;
    }
}
