using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoJumpZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerStats.jump = false;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerStats.jump = true;
    }
}
