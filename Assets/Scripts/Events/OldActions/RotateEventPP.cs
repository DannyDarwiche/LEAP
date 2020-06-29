using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEventPP : MonoBehaviour
{
    //Isak
    //Rotates object when activated by preassure plate.

    [SerializeField]
    float id;
    [SerializeField]
    Vector3 rotationVector;
    [SerializeField]
    float rotationSpeed;

    bool activated;

    void Start()
    {
        GameEvents.currentInstance.OnPreasureplateTriggerOn += Activated;
        GameEvents.currentInstance.OnPreasureplateTriggerOff += Deactivated;
    }

    void Update()
    {
        if(activated)
            transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }

    void Activated(float id, float percentage)
    {
        if (id == this.id)
            activated = true;
    }

    void Deactivated(float id, float percentage)
    {
        if (id == this.id)
            if(percentage == 0)
                activated = false;
    }
}
