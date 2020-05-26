using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
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

    public event Action<int> OnButtonTriggerOn;
    public void ButtonTriggerOn(int id)
    {
        if (OnButtonTriggerOn != null)
            OnButtonTriggerOn(id);
    }

    public event Action<int> OnButtonTriggerOff;
    public void ButtonTriggerOff(int id)
    {
        if (OnButtonTriggerOff != null)
            OnButtonTriggerOff(id);
    }

    public event Action<int, float> OnPreasureplateTriggerOn;
    public void PreasureplateTriggerOn(int id, float percentage)
    {
        if (OnPreasureplateTriggerOn != null)
            OnPreasureplateTriggerOn(id, percentage);
    }

    public event Action<int, float> OnPreasureplateTriggerOff;
    public void PreasureplatTriggerOff(int id, float percentage)
    {
        if (OnPreasureplateTriggerOff != null)
            OnPreasureplateTriggerOff(id, percentage);
    }

    public event Action<int> OnPuzzleSolvedTrigger;
    public void PuzzleSolvedTrigger(int id)
    {
        if (OnPuzzleSolvedTrigger != null)
            OnPuzzleSolvedTrigger(id);
    }

    public event Action<int> OnPuzzleFailedTrigger;
    public void PuzzleFailedTrigger(int id)
    {
        if (OnPuzzleFailedTrigger != null)
            OnPuzzleFailedTrigger(id);
    }


    public event Action<int> OnPlatformTriggerOn; 
    public void PlatformTriggerOn(int id)
    {
        if (OnPlatformTriggerOn != null)
            OnPlatformTriggerOn(id); 
    }

    public event Action<int> OnPlatformTriggerOff; 
    public void PlatformTriggerOff(int id)
    {
        if (OnPlatformTriggerOff != null)
            OnPlatformTriggerOff(id); 
    }
}
