using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TokenCount : MonoBehaviour
{
    //Isak and Danny
    //Updates the players total upgrade token count.

    [SerializeField]
    string name;

    [SerializeField]
    string description;

    [SerializeField]
    Sprite image;

    UIManager uiManager;

    void Start()
    {
        GameEvents.currentInstance.OnTokenGet += CallFromToken;
        uiManager = Camera.main.GetComponent<UIManager>();
        PlayerStats.UpdateTokenCount(0);
    }

    void CallFromToken()
    {
        PlayerStats.UpdateTokenCount(1);
        uiManager.DisplayUpgradeInfo(name, description, image);
    }
}
