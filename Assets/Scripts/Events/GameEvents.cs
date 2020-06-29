using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    //Isak, Danny, Ai, Alexander and Muhammad
    //Collection of all custom events.

    public static GameEvents currentInstance;

    void Awake()
    {
        if (currentInstance == null)
            currentInstance = this;
    }

    public event Action OnTokenGet;
    public void TokenGet()
    {
        if (OnTokenGet != null)
            OnTokenGet();
    }

    public event Action<AbilityType> OnUpgradeGet;
    public void UpgradeGet(AbilityType abilityType)
    {
        if (OnUpgradeGet != null)
            OnUpgradeGet(abilityType);
    }

    public event Action<float> OnButtonTriggerOn;
    public void ButtonTriggerOn(float id)
    {
        if (OnButtonTriggerOn != null)
            OnButtonTriggerOn(id);
    }

    public event Action<float> OnButtonTriggerOff;
    public void ButtonTriggerOff(float id)
    {
        if (OnButtonTriggerOff != null)
            OnButtonTriggerOff(id);
    }

    public event Action<float, float> OnPreasureplateTriggerOn;
    public void PreasureplateTriggerOn(float id, float percentage)
    {
        if (OnPreasureplateTriggerOn != null)
            OnPreasureplateTriggerOn(id, percentage);
    }

    public event Action<float, float> OnPreasureplateTriggerOff;
    public void PreasureplateTriggerOff(float id, float percentage)
    {
        if (OnPreasureplateTriggerOff != null)
            OnPreasureplateTriggerOff(id, percentage);
    }

    public event Action<float> OnPuzzleSolvedTrigger;
    public void PuzzleSolvedTrigger(float id)
    {
        if (OnPuzzleSolvedTrigger != null)
            OnPuzzleSolvedTrigger(id);
    }

    public event Action<float> OnPuzzleFailedTrigger;
    public void PuzzleFailedTrigger(float id)
    {
        if (OnPuzzleFailedTrigger != null)
            OnPuzzleFailedTrigger(id);
    }

    //GreenKeyPlatform
    public event Action<float> OnPlatformTriggerOn; 
    public void PlatformTriggerOn(float id)
    {
        if (OnPlatformTriggerOn != null)
            OnPlatformTriggerOn(id); 
    }

    //GreenKeyPlatform
    public event Action<float> OnPlatformTriggerOff; 
    public void PlatformTriggerOff(float id)
    {
        if (OnPlatformTriggerOff != null)
            OnPlatformTriggerOff(id); 
    }

    public event Action<float> OnTriggerOn;
    public void TriggerOn(float id)
    {
        if (OnTriggerOn != null)
            OnTriggerOn(id);
    }
    public event Action<float> OnTriggerOff;
    public void TriggerOff(float id)
    {
        if (OnTriggerOff != null)
            OnTriggerOff(id);
    }
}
