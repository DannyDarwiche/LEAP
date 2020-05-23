using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    [SerializeField]
    MovingCharacter player;

    List<Button> buttons = new List<Button>();

    PlayerStats playerStats;

    void Start()
    {
        SetPlayerStats(player.GetPlayerStats());
    }

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            buttons.Add(transform.GetChild(i).GetComponent<Button>());
        }
    }

    public void SetPlayerStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }

    public void ClickGetUpgrade(int i)
    {
        playerStats.UnlockAbility((AbilityType)i);
        Debug.Log("Jump Gotten! " + i);
    }
}
