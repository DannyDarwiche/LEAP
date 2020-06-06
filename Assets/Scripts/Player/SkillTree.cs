using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    //Isak and Danny
    //Skill tree that keeps track of all the players upgrades.
    //Purchased or picked up upgrades are activated.
    //Upgrade menu ui is updated.

    public static SkillTree instance;

    [SerializeField]
    MovingCharacter player;

    [SerializeField]
    Color lockedColor;
    [SerializeField]
    Color unlockableIconColor;

    [Header("MAIN upgrades colors")]
    [SerializeField]
    Color unlockedMainButtonColor;
    [SerializeField]
    Color unlockableMainButtonColor;

    [Header("SUB upgrades color")]
    [SerializeField]
    Color unlockedSubButtonColor;
    [SerializeField]
    Color unlockableSubButtonColor;

    List<Upgrade> upgrades = new List<Upgrade>();
    bool allFinished = false;

    void Awake()
    {
        if (instance == null)
            instance = this;

        GameObject[] list = GameObject.FindGameObjectsWithTag("UpgradeButton");
        for (int i = 0; i < list.Length; i++)
            upgrades.Add(list[i].GetComponent<Upgrade>());

        UpdateUI();
    }

    void Start()
    {
        StartCoroutine(WaitUntilAllFinished());
    }

    IEnumerator WaitUntilAllFinished()
    {
        while (!allFinished)
        {
            allFinished = CheckIfCompleted();
            yield return null;
        }
    }

    bool CheckIfCompleted()
    {
        foreach (Upgrade upgrade in upgrades)
            if (!upgrade.finished)
                return false;

        return true;
    }

    public bool IsUnlocked(AbilityType requiredAbility)
    {
        if (requiredAbility == AbilityType.None)
            return true;

        foreach (Upgrade u in upgrades)
            if (u.abilityType == requiredAbility)
                return u.Unlocked();
        return false;
    }

    public void UpdateUI()
    {
        foreach (Upgrade u in upgrades)
        {
            if (u.Unlocked())
            {
                //Button Image Color
                Button button = u.gameObject.GetComponent<Button>();
                ColorBlock colors = button.colors;
                if (u.mainAbility)
                    colors.disabledColor = unlockedMainButtonColor;
                else
                    colors.disabledColor = unlockedSubButtonColor;
                button.colors = colors;

                //Icon Image Color
                Image[] images = u.gameObject.GetComponentsInChildren<Image>();
                images[1].color = unlockableIconColor;

                u.gameObject.GetComponent<Button>().interactable = false;
            }
            else if (IsUnlocked(u.requiredAbility) && !u.mainAbility)
            {
                //Button Image Color
                Button button = u.gameObject.GetComponent<Button>();
                ColorBlock colors = button.colors;
                if (u.mainAbility)
                    colors.normalColor = unlockableMainButtonColor;
                else
                    colors.normalColor = unlockableSubButtonColor;
                button.colors = colors;

                //Icon Image Color
                Image[] images = u.gameObject.GetComponentsInChildren<Image>();
                images[1].color = unlockableIconColor;

                u.gameObject.GetComponent<Button>().interactable = true;
            }
            else
            {
                //Button Image Color
                Button button = u.gameObject.GetComponent<Button>();
                ColorBlock colors = button.colors;
                colors.disabledColor = lockedColor;
                button.colors = colors;

                //Icon Image Color
                Image[] images = u.gameObject.GetComponentsInChildren<Image>();
                images[1].color = lockedColor;

                u.gameObject.GetComponent<Button>().interactable = false;
            }
        }

    }

    public void SetUpgrades(AbilityType abilityType)
    {
        SkillTree.instance.UpdateUI();

        switch (abilityType)
        {
            case AbilityType.Jump:
                PlayerStats.jump = true;
                break;
            case AbilityType.JumpHeightUp1:
                SetJumpHeight();
                break;
            case AbilityType.AirJumpUp1:
                SetAirJump();
                break;
            case AbilityType.JumpHeightUp2:
                SetJumpHeight();
                break;
            case AbilityType.AirJumpUp2:
                SetAirJump();
                break;
            case AbilityType.Dash:
                PlayerStats.dash = true;
                break;
            case AbilityType.AddedDash:
                SetMaxDashes();
                break;
            case AbilityType.DashCooldown:
                SetDashCooldown();
                break;
            case AbilityType.DashDurationUp:
                SetDashDuration();
                break;
            case AbilityType.PickUp:
                SetPickUp();
                break;
            case AbilityType.StrengthUp:

                break;
            case AbilityType.GrapplingGun:

                break;
            case AbilityType.GrapplingGunPull:

                break;
            case AbilityType.SlowTime:

                break;
            case AbilityType.SlowTimeUp:

                break;
            case AbilityType.ReverseTime:

                break;
            case AbilityType.ReverseTimeUp:

                break;
            case AbilityType.Move:

                break;
            case AbilityType.MoveSpeedUp1:
                SetMaxSpeed();
                break;
            case AbilityType.Crouch:
                SetCrouch();
                break;
            case AbilityType.Sprint:
                PlayerStats.sprint = true;
                break;
            case AbilityType.MoveSpeedUp2:
                SetMaxSpeed();
                break;
            case AbilityType.WallJump:
                PlayerStats.walljump = true;
                break;
            case AbilityType.WallJumpAngleUp:

                break;
        }
    }

    void SetJumpHeight()
    {
        player.jumpHeight = player.jumpHeight + 1;
    }
    void SetAirJump()
    {
        player.maxAirJumps++;
    }
    void SetMaxDashes()
    {
        player.dashPhase++;
    }
    void SetDashCooldown()
    {
        player.dashCooldown = player.dashCooldown / 2;
    }
    void SetDashDuration()
    {
        player.dashDuration = player.dashDuration * 2;
    }
    void SetPickUp()
    {
        player.GetComponentInChildren<PickUpManager>().enabled = true;
    }
    void SetMaxSpeed()
    {
        player.maxSpeed = player.maxSpeed + 1;
    }
    void SetCrouch()
    {
        player.GetComponent<Crouch>().enabled = true;
    }
}