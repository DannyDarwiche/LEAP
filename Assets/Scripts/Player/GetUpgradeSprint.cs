using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgradeSprint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.sprint = true;
            Destroy(gameObject);
        }
    }
}
