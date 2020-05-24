using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    [SerializeField]
    Material lockedAbilityMaterial;

    [SerializeField]
    Material unlockableAbilityMaterial;

    [SerializeField]
    Material unlockedAbilityMaterial;

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
        Button[] buttonsArray = transform.GetComponentsInChildren<Button>();
        foreach (Button b in buttonsArray)
        {
            buttons.Add(b);
        }

        //for (int i = 0; i < transform.GetComponentsInChildren<Button>().GetLength(0) - 1; i++)
        //{
        //    buttons.Add(transform.GetChild(i).GetComponent<Button>());
        //}
    }

    public void SetPlayerStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
        this.playerStats.TryUnlockAbility(AbilityType.Move);
        playerStats.OnAbilityUnlocked += PlayerStatsOnAbilityUnlocked;
        UpdateUI();

    }

    void PlayerStatsOnAbilityUnlocked(object sender, PlayerStats.OnAbilityUnlockedEventArgs e)
    {
        UpdateUI();
    }

    public void ClickGetUpgrade(int i)
    {
        if (!playerStats.TryUnlockAbility((AbilityType)i))
        {
            //Add warning!
            Debug.Log("Cannot Unlock Yet");
        }
        else
            Debug.Log("New Ability " + i);
        //Debug.Log(buttons.Count);
    }

    void UpdateUI()
    {
        //i in the For loop starts at 1 because the AbilityType enum 0 is AbilityType.None which doesnt exist.
        //i as index for the Buttons list is -1 since there is no button for AbilityType.None.
        for (int i = 1; i <= buttons.Count; i++)
        {
            if (playerStats.IsAbilityUnlocked((AbilityType)i))
            {
                buttons[i - 1].gameObject.GetComponent<Image>().material = unlockedAbilityMaterial;
            }
            else
            {
                if (playerStats.CanUnlock((AbilityType)i))
                {
                    buttons[i - 1].gameObject.GetComponent<Image>().material = unlockableAbilityMaterial;
                }
                else
                {
                    buttons[i - 1].gameObject.GetComponent<Image>().material = lockedAbilityMaterial;
                }
            }
        }
    }
}
