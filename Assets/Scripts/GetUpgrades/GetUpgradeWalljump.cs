using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgradeWalljump : MonoBehaviour
{
    [SerializeField]
    string upgradeName;

    [SerializeField]
    string upgradeDescription;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.walljump = true;
            other.GetComponentInChildren<UIManager>().DisplayUpgradeInfo(upgradeName, upgradeDescription);
            Destroy(gameObject);
        }
    }
}
