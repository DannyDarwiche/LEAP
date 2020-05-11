using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableEvent : MonoBehaviour
{
    [SerializeField]
    int id;

    Renderer objectRenderer;
    Collider objectCollider;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();
        GameEvents.currentInstance.OnButtonTriggerOn += Activated;
        GameEvents.currentInstance.OnButtonTriggerOff += Deactivated;
    }

    void Activated(int id)
    {
        if(id == this.id)
        {
            objectRenderer.enabled = false;
            objectCollider.enabled = false;
        }
    }

    void Deactivated(int id)
    {
        if (id == this.id)
        {
            objectRenderer.enabled = true;
            objectCollider.enabled = true;
        }
    }
}
