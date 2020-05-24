using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgradeToken : MonoBehaviour
{
    [SerializeField]
    string upgradeName;

    [SerializeField]
    string upgradeDescription;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.upgradeTokens++;
            other.GetComponentInChildren<UIManager>().DisplayUpgradeInfo(upgradeName, upgradeDescription);
            Destroy(gameObject);
        }
    }
}
