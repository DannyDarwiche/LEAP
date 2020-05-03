using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgradeJump : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.jump = true;
            Destroy(gameObject);
        }
    }
}
