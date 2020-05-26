using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgradeToken : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.currentInstance.TokenGet();
            Destroy(gameObject);
        }
    }
}
