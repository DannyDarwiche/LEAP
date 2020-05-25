using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum AbilityType
{
    None,
    Jump,
    JumpHeightUp1,
    AirJumpUp1,
    JumpHeightUp2,
    AirJumpUp2,
    Dash,
    AddedDash,
    DashCooldown,
    DashDurationUp,
    PickUp,
    StrengthUp,
    GrapplingGun,
    GrapplingGunPull,
    SlowTime,
    SlowTimeUp,
    ReverseTime,
    ReverseTimeUp,
    Move,
    MoveSpeedUp1,
    Crouch,
    Sprint,
    MoveSpeedUp2,
    WallJump,
    WallJumpAngleUp,
}

public class PlayerStats : MonoBehaviour
{
    public static int upgradeTokens = 100;

    public static bool jump = false;

    public static bool sprint = false;

    public static bool walljump = false;

    public static bool dash = false;

    public static int maxAirJumps = 0;

    [SerializeField]
    MovingCharacter player;

    [SerializeField]
    GameObject tokenCountImage;

    static TextMeshProUGUI tokenCountText;

    public static void UpdateTokenCount(int tokenAmount)
    {
        upgradeTokens += tokenAmount;
        tokenCountText.text = upgradeTokens.ToString();
    }

    void Awake()
    {
        tokenCountText = tokenCountImage.GetComponentInChildren<TextMeshProUGUI>();
    }

    //public void SetUpgrades(AbilityType abilityType)
    //{
    //    switch (abilityType)
    //    {
    //        case AbilityType.Jump:
    //            jump = true;
    //            break;
    //        case AbilityType.JumpHeightUp1:
    //            SetJumpHeight();
    //            break;
    //        case AbilityType.AirJumpUp1:
    //            SetAirJump();
    //            break;
    //        case AbilityType.JumpHeightUp2:
    //            SetJumpHeight();
    //            break;
    //        case AbilityType.AirJumpUp2:
    //            SetAirJump();
    //            break;
    //        case AbilityType.Dash:
    //            dash = true;
    //            break;
    //        case AbilityType.AddedDash:
    //            SetMaxDashes();
    //            break;
    //        case AbilityType.DashCooldown:
    //            SetDashCooldown();
    //            break;
    //        case AbilityType.DashDurationUp:
    //            SetDashDuration();
    //            break;
    //        case AbilityType.PickUp:
    //            SetPickUp();
    //            break;
    //        case AbilityType.StrengthUp:

    //            break;
    //        case AbilityType.GrapplingGun:

    //            break;
    //        case AbilityType.GrapplingGunPull:

    //            break;
    //        case AbilityType.SlowTime:

    //            break;
    //        case AbilityType.SlowTimeUp:

    //            break;
    //        case AbilityType.ReverseTime:

    //            break;
    //        case AbilityType.ReverseTimeUp:

    //            break;
    //        case AbilityType.Move:

    //            break;
    //        case AbilityType.MoveSpeedUp1:
    //            SetMaxSpeed();
    //            break;
    //        case AbilityType.Crouch:
    //            SetCrouch();
    //            break;
    //        case AbilityType.Sprint:
    //            sprint = true;
    //            break;
    //        case AbilityType.MoveSpeedUp2:
    //            SetMaxSpeed();
    //            break;
    //        case AbilityType.WallJump:
    //            walljump = true;
    //            break;
    //        case AbilityType.WallJumpAngleUp:

    //            break;
    //    }
    //}

    //void SetJumpHeight()
    //{
    //    player.jumpHeight = jumpHeight + 1;
    //}
    //void SetAirJump()
    //{
    //    player.maxAirJumps++;
    //}
    //void SetMaxDashes()
    //{
    //    player.dashPhase++;
    //}
    //void SetDashCooldown()
    //{
    //    player.dashCooldown = player.dashCooldown / 2;
    //}
    //void SetDashDuration()
    //{
    //    player.dashDuration = player.dashDuration * 2;
    //}
    //void SetPickUp()
    //{
    //    player.GetComponentInChildren<PickUpManager>().enabled = true;
    //}
    //void SetMaxSpeed()
    //{
    //    player.maxSpeed = player.maxSpeed + 1;
    //}
    //void SetCrouch()
    //{
    //    player.GetComponent<Crouch>().enabled = true;
    //}

}



//List<AbilityType> unlockedAbilitiesList;

//public PlayerStats()
//{
//    unlockedAbilitiesList = new List<AbilityType>();
//}

//void UnlockAbility(AbilityType abilityType)
//{
//    if (!IsAbilityUnlocked(abilityType))
//    {
//        unlockedAbilitiesList.Add(abilityType);
//        OnAbilityUnlocked?.Invoke(this, new OnAbilityUnlockedEventArgs { abilityType = abilityType });
//    }
//}

//public bool IsAbilityUnlocked(AbilityType abilityType)
//{
//    return unlockedAbilitiesList.Contains(abilityType);
//}

//public bool CanUnlock(AbilityType abilityType)
//{
//    AbilityType abilityRequirement = GetAbilityRequirement(abilityType);

//    if (abilityRequirement != AbilityType.None)
//    {
//        if (IsAbilityUnlocked(abilityRequirement))
//        {
//            return true;
//        }
//        else
//            return false;
//    }
//    else
//    {
//        return true;
//    }
//}

//public event EventHandler<OnAbilityUnlockedEventArgs> OnAbilityUnlocked;
//public class OnAbilityUnlockedEventArgs : EventArgs
//{
//    public AbilityType abilityType;
//}

//public AbilityType GetAbilityRequirement(AbilityType abilityType)
//{
//    switch (abilityType)
//    {
//        case AbilityType.JumpHeightUp1:
//            return AbilityType.Jump;
//        case AbilityType.AirJumpUp1:
//            return AbilityType.Jump;
//        case AbilityType.Dash:
//            return AbilityType.Jump;
//        case AbilityType.JumpHeightUp2:
//            return AbilityType.AirJumpUp1;
//        case AbilityType.AirJumpUp2:
//            return AbilityType.AirJumpUp1;
//        case AbilityType.MoveSpeedUp1:
//            return AbilityType.Move;
//        case AbilityType.Crouch:
//            return AbilityType.Move;
//        case AbilityType.MoveSpeedUp2:
//            return AbilityType.Sprint;
//        case AbilityType.AddedDash:
//            return AbilityType.Dash;
//        case AbilityType.DashCooldown:
//            return AbilityType.Dash;
//        case AbilityType.DashForceUp:
//            return AbilityType.Dash;
//        case AbilityType.WallJumpAngleUp:
//            return AbilityType.WallJump;
//        case AbilityType.StrengthUp:
//            return AbilityType.PickUp;
//        case AbilityType.GrapplingGunPull:
//            return AbilityType.GrapplingGun;
//        case AbilityType.SlowTimeUp:
//            return AbilityType.SlowTime;
//        case AbilityType.ReverseTimeUp:
//            return AbilityType.ReverseTime;
//    }
//    return AbilityType.None;
//}

//public bool TryUnlockAbility(AbilityType abilityType)
//{
//    if (CanUnlock(abilityType))
//    {
//        UnlockAbility(abilityType);
//        return true;
//    }
//    else
//        return false;
//}

