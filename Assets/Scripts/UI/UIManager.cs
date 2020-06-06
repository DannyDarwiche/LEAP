using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Isak and Danny
    //Displays the upgrade or token info when picking up an orb.

    [SerializeField]
    Image upgradeAcquiredImage;
    [SerializeField]
    TextMeshProUGUI upgradeAcquiredName;
    [SerializeField]
    TextMeshProUGUI upgradeAcquiredDescription;
    [SerializeField, Range(0f, 10f)]
    float acquiredUITimeDelay = 5f;

    public void DisplayUpgradeInfo(string name, string description, Sprite image)
    {
        upgradeAcquiredName.text = name;
        upgradeAcquiredDescription.text = description;
        upgradeAcquiredImage.sprite = image;
        upgradeAcquiredName.enabled = true;
        upgradeAcquiredDescription.enabled = true;
        upgradeAcquiredImage.enabled = true;

        StartCoroutine(DisplayTimer());
    }

    IEnumerator DisplayTimer()
    {
        yield return new WaitForSeconds(acquiredUITimeDelay);

        upgradeAcquiredName.enabled = false;
        upgradeAcquiredDescription.enabled = false;
        upgradeAcquiredImage.enabled = false;
    }

    void Awake()
    {
        upgradeAcquiredName.enabled = false;
        upgradeAcquiredDescription.enabled = false;
        upgradeAcquiredImage.enabled = false;
    }

}
