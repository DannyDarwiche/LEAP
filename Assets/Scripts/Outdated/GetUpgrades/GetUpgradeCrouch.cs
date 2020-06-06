using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgradeCrouch : MonoBehaviour
{
    [SerializeField]
    string upgradeName;

    [SerializeField]
    string upgradeDescription;

    [SerializeField]
    int upgradePrice;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerStats.upgradeTokens >= upgradePrice)
        {
            PlayerStats.upgradeTokens -= upgradePrice;
            other.GetComponentInChildren<Crouch>().enabled = true;
            //other.GetComponentInChildren<UIManager>().DisplayUpgradeInfo(upgradeName, upgradeDescription);
            Destroy(gameObject);
        }
    }
}
