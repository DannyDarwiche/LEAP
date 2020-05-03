using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPickUpUpgrade : MonoBehaviour
{
    //[SerializeField]
    //PickUpManager playerPickUpManager;

    [SerializeField]
    string upgradeName;

    [SerializeField]
    string upgradeDescription;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<PickUpManager>().enabled = true;
            other.GetComponentInChildren<UIManager>().DisplayUpgradeInfo(upgradeName, upgradeDescription);
            Destroy(gameObject);
        }
    }
}
