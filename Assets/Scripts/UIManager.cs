using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //[SerializeField]
    //TextMeshProUGUI storeItemPrice;
    //[SerializeField]
    //TextMeshProUGUI storeItemName;
    //[SerializeField]
    //TextMeshProUGUI storeItemDescription;
    //[SerializeField]
    //TextMeshProUGUI playerTokenCount;
    [SerializeField]
    TextMeshProUGUI upgradeAcquiredName;
    [SerializeField]
    TextMeshProUGUI upgradeAcquiredDescription;
    [SerializeField, Range(0f, 10f)]
    float acquiredUITimeDelay = 5f;
    //[SerializeField]
    //LayerMask storeLayer;

    //bool displayAcquiredUpgrade = false;
    //float acquiredUITimer = 0;

    void Awake()
    {
        //SetActive(false);

        upgradeAcquiredName.enabled = false;
        upgradeAcquiredDescription.enabled = false;
    }

    //void Update()
    //{
    //    Raycast();

    //    if (displayAcquiredUpgrade)
    //    {
    //        acquiredUITimer += Time.deltaTime;
    //        if (acquiredUITimer >= acquiredUITimeDelay)
    //        {
    //            upgradeAcquiredName.enabled = false;
    //            upgradeAcquiredDescription.enabled = false;
    //            displayAcquiredUpgrade = false;
    //        }
    //    }
    //}

    public void DisplayUpgradeInfo(string name, string description)
    {
        //acquiredUITimer = 0;
        upgradeAcquiredName.text = name;
        upgradeAcquiredDescription.text = description;
        upgradeAcquiredName.enabled = true;
        upgradeAcquiredDescription.enabled = true;

        Debug.Log("displayUpgradeInfo");

        StartCoroutine(DisplayTimer());
        //displayAcquiredUpgrade = true;
    }

    IEnumerator DisplayTimer()
    {
        yield return new WaitForSeconds(acquiredUITimeDelay);

        upgradeAcquiredName.enabled = false;
        upgradeAcquiredDescription.enabled = false;
    }

    //void Raycast()
    //{
    //    RaycastHit hit;
    //    Vector3 fwd = transform.TransformDirection(Vector3.forward);

    //    if (Physics.Raycast(transform.position, fwd, out hit, 10, storeLayer))
    //    {
    //        SetActive(true);

    //        StoreInfo storeInfo = hit.transform.GetComponent<StoreInfo>();
    //        storeItemPrice.text = "Price: " + storeInfo.GetPrice().ToString();
    //        storeItemName.text = storeInfo.GetName();
    //        storeItemDescription.text = storeInfo.GetDescription();
    //        playerTokenCount.text = "Tokens Left: " + PlayerStats.upgradeTokens.ToString();
    //    }
    //    else
    //        SetActive(false);
    //}

    //void SetActive(bool status)
    //{
    //    storeItemPrice.enabled = status;
    //    storeItemName.enabled = status;
    //    storeItemDescription.enabled = status;
    //    playerTokenCount.enabled = status;
    //}

}
