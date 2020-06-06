using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour
{
    //Isak and Danny
    //Template for an upgrade that sits on a UI button.
    //Is customized in the editor.

    [HideInInspector]
    public bool finished = false;

    [SerializeField]
    public AbilityType abilityType;
    [SerializeField]
    public AbilityType requiredAbility;
    [SerializeField]
    public bool mainAbility;
    [SerializeField]
    int cost;
    [SerializeField]
    string name;
    [SerializeField]
    string description;
    [SerializeField]
    Sprite image;

    bool unlocked;

    float transformWidth;
    float transformHeight;

    UIManager uiManager;

    void Start()
    {
        uiManager = Camera.main.GetComponent<UIManager>();
        GameEvents.currentInstance.OnUpgradeGet += CallFromOrb;
        transformWidth = gameObject.GetComponent<RectTransform>().rect.width;
        transformHeight = gameObject.GetComponent<RectTransform>().rect.height;

        if (abilityType == AbilityType.Move)
            OnClick();

        finished = true;
    }

    void CallFromOrb(AbilityType abilityType)
    {
        if (this.abilityType == abilityType)
        {
            OnClick();
            uiManager.DisplayUpgradeInfo(name, description, image);
        }
    }

    public void OnClick()
    {
        if (PlayerStats.upgradeTokens >= cost && SkillTree.instance.IsUnlocked(requiredAbility))
        {
            unlocked = true;
            PlayerStats.UpdateTokenCount(-cost);
            SkillTree.instance.SetUpgrades(abilityType);
        }
    }

    public bool Unlocked()
    {
        return unlocked;
    }

    public void OnHoverEnter()
    {
        HoverPanel.instance.gameObject.SetActive(true);
        HoverPanel.instance.UpdatePanel(name, description, cost, transform.TransformVector(transform.position), transformWidth, unlocked);
    }

    public void OnHoverExit()
    {
        HoverPanel.instance.gameObject.SetActive(false);
    }
}
