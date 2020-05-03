using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInfo : MonoBehaviour
{
    //Scriptable objects?

    [SerializeField]
    int price;

    [SerializeField]
    string upgradeName;

    [SerializeField]
    string upgradeDescription;

    public int GetPrice()
    {
        return price;
    }

    public string GetName()
    {
        return upgradeName;
    }

    public string GetDescription()
    {
        return upgradeDescription;
    }
}
