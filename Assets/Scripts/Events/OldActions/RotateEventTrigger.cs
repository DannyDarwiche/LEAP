using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEventTrigger : MonoBehaviour
{
    //Isak and Danny
    //Rotates object when activated by trigger.

    [SerializeField]
    float id;
    [SerializeField]
    Vector3 rotationVector;
    [SerializeField]
    float rotationSpeed;

    bool activated;

    void Start()
    {
        GameEvents.currentInstance.OnTriggerOn += Activated;
        GameEvents.currentInstance.OnTriggerOff += Deactivated;
    }

    void Update()
    {
        if (activated)
            transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }

    void Activated(float id)
    {
        if (id == this.id)
            activated = true;
    }

    void Deactivated(float id)
    {
        if (id == this.id)
            activated = false;
    }
}

