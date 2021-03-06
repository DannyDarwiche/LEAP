﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableEvent : MonoBehaviour
{
    //Isak
    //Enables and disables objects when a button is pressed.

    [SerializeField]
    float id;

    Renderer objectRenderer;
    Collider objectCollider;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();
        GameEvents.currentInstance.OnButtonTriggerOn += Activated;
        GameEvents.currentInstance.OnButtonTriggerOff += Deactivated;
    }

    void Activated(float id)
    {
        if(id == this.id)
        {
            objectRenderer.enabled = false;
            objectCollider.enabled = false;
        }
    }

    void Deactivated(float id)
    {
        if (id == this.id)
        {
            objectRenderer.enabled = true;
            objectCollider.enabled = true;
        }
    }
}
