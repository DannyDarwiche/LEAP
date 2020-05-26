using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TokenCount : MonoBehaviour
{
    [SerializeField]
    string name;

    [SerializeField]
    string description;

    [SerializeField]
    Sprite image;

    //[SerializeField]
    //GameObject image;

    UIManager uiManager;

    void Start()
    {
        GameEvents.currentInstance.OnTokenGet += CallFromToken;
        uiManager = Camera.main.GetComponent<UIManager>();
        PlayerStats.UpdateTokenCount(0);

        //image.SetActive(false);
    }

    void CallFromToken()
    {
        PlayerStats.UpdateTokenCount(1);
        uiManager.DisplayUpgradeInfo(name, description, image);
    }
}
