using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpgradeJumpHeight : MonoBehaviour
{
    [SerializeField]
    string upgradeName;

    [SerializeField]
    string upgradeDescription;

    [SerializeField]
    int upgradePrice;

    [SerializeField]
    float addedJumpHeight = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerStats.upgradeTokens >= upgradePrice)
        {
            PlayerStats.upgradeTokens -= upgradePrice;
            PlayerStats.jumpHeight += addedJumpHeight;
            other.GetComponentInChildren<MovingCharacter>().jumpHeight = PlayerStats.jumpHeight;
            other.GetComponentInChildren<UIManager>().DisplayUpgradeInfo(upgradeName, upgradeDescription);
            Destroy(gameObject);
        }
    }
}
