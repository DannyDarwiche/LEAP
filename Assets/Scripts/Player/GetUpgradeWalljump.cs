using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgradeWalljump : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.walljump = true;
        }
        Destroy(gameObject);
    }
}
