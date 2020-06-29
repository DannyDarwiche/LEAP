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
    //Isak and Danny
    //Collection of all the players possible upgrades and abilities. 

    public static int upgradeTokens = 50;

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
}