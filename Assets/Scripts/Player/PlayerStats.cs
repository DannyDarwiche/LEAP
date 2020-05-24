using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    DashForceUp,
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

    List<AbilityType> unlockedAbilitiesList;

    public PlayerStats()
    {
        unlockedAbilitiesList = new List<AbilityType>();
    }

    void UnlockAbility(AbilityType abilityType)
    {
        if (!IsAbilityUnlocked(abilityType))
        {
            unlockedAbilitiesList.Add(abilityType);
            OnAbilityUnlocked?.Invoke(this, new OnAbilityUnlockedEventArgs { abilityType = abilityType });
        }
    }

    public bool IsAbilityUnlocked(AbilityType abilityType)
    {
        return unlockedAbilitiesList.Contains(abilityType);
    }

    public bool CanUnlock(AbilityType abilityType)
    {
        AbilityType abilityRequirement = GetAbilityRequirement(abilityType);

        if (abilityRequirement != AbilityType.None)
        {
            if (IsAbilityUnlocked(abilityRequirement))
            {
                return true;
            }
            else
                return false;
        }
        else
        {
            return true;
        }
    }

    public event EventHandler<OnAbilityUnlockedEventArgs> OnAbilityUnlocked;
    public class OnAbilityUnlockedEventArgs : EventArgs
    {
        public AbilityType abilityType;
    }

    public AbilityType GetAbilityRequirement(AbilityType abilityType)
    {
        switch (abilityType)
        {
            case AbilityType.JumpHeightUp1:
                return AbilityType.Jump;
            case AbilityType.AirJumpUp1:
                return AbilityType.Jump;
            case AbilityType.Dash:
                return AbilityType.Jump;
            case AbilityType.JumpHeightUp2:
                return AbilityType.AirJumpUp1;
            case AbilityType.AirJumpUp2:
                return AbilityType.AirJumpUp1;
            case AbilityType.MoveSpeedUp1:
                return AbilityType.Move;
            case AbilityType.Crouch:
                return AbilityType.Move;
            case AbilityType.MoveSpeedUp2:
                return AbilityType.Sprint;
            case AbilityType.AddedDash:
                return AbilityType.Dash;
            case AbilityType.DashCooldown:
                return AbilityType.Dash;
            case AbilityType.DashForceUp:
                return AbilityType.Dash;
            case AbilityType.WallJumpAngleUp:
                return AbilityType.WallJump;
            case AbilityType.StrengthUp:
                return AbilityType.PickUp;
            case AbilityType.GrapplingGunPull:
                return AbilityType.GrapplingGun;
            case AbilityType.SlowTimeUp:
                return AbilityType.SlowTime;
            case AbilityType.ReverseTimeUp:
                return AbilityType.ReverseTime;
        }
        return AbilityType.None;
    }

    public bool TryUnlockAbility(AbilityType abilityType)
    {
        if (CanUnlock(abilityType))
        {
            UnlockAbility(abilityType);
            return true;
        }
        else
            return false;
    }





    public static int upgradeTokens = 0;

    public static bool jump = false;

    public static bool sprint = false;

    public static float jumpHeight = 3;

    public static int maxAirJumps = 0;

    public static bool walljump = false;
}
