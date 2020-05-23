using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum AbilityType
    {
        Jump,
        Dash,
        WallJump,
        IncreasedSpeed,
        IncreasedJumpHeight
    }
public class PlayerStats : MonoBehaviour
{

    List<AbilityType> unlockedAbilitiesList;

    public PlayerStats()
    {
        unlockedAbilitiesList = new List<AbilityType>();
    }

    public void UnlockAbility(AbilityType ability)
    {
        if (!IsAbilityUnlocked(ability))
        {
            unlockedAbilitiesList.Add(ability);
            OnAbilityUnlocked?.Invoke(this, new OnAbilityUnlockedEventArgs { abilityType = ability });
        }
    }

    public bool IsAbilityUnlocked(AbilityType ability)
    {
        return unlockedAbilitiesList.Contains(ability);
    }

    public static int upgradeTokens = 0;

    public static bool jump = false;

    public static bool sprint = false;

    public static float jumpHeight = 3;

    public static int maxAirJumps = 0;

    public static bool walljump = false;

    public event EventHandler<OnAbilityUnlockedEventArgs> OnAbilityUnlocked;
    public class OnAbilityUnlockedEventArgs : EventArgs
    {
        public AbilityType abilityType;
    }
}
