using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents currentInstance;


    void Awake()
    {
        if(currentInstance == null)
            currentInstance = this; 

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
            OnPreasureplateTriggerOn(id,percentage);
    }

    public event Action<int, float> OnPreasureplateTriggerOff;
    public void PreasureplatTriggerOff(int id, float percentage)
    {
        if (OnPreasureplateTriggerOff != null)
            OnPreasureplateTriggerOff(id, percentage);
    }

}
