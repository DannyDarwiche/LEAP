using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRotateEvent : Event
{
    //Isak
    //Rotates object when activated by button.

    [SerializeField]
    Vector3 rotationVector;
    [SerializeField]
    float rotationSpeed;
    [Header("PartialRotation")]
    [SerializeField]
    bool partialRotation;
    [SerializeField]
    Vector3 maxRotation;

    float time;

    void Start()
    {
        GameEvents.currentInstance.OnButtonTriggerOn += Activated;
        GameEvents.currentInstance.OnButtonTriggerOff += Deactivated;
        GameEvents.currentInstance.OnPreasureplateTriggerOn += Activated;
        GameEvents.currentInstance.OnPreasureplateTriggerOff += Deactivated;
        GameEvents.currentInstance.OnTriggerOn += Activated;
        GameEvents.currentInstance.OnTriggerOff += Deactivated;
    }

    void Update()
    {
        if (activated)
        {
            if (partialRotation)
            {
                time += Time.deltaTime;

                transform.localRotation = Quaternion.Euler(maxRotation.x * Mathf.Sin(time * rotationSpeed), 
                    maxRotation.y * Mathf.Sin(time * rotationSpeed), 
                    maxRotation.z * Mathf.Sin(time * rotationSpeed));
            }
            else
                transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
        }
    }

    void OnDestroy()
    {
        GameEvents.currentInstance.OnButtonTriggerOn -= Activated;
        GameEvents.currentInstance.OnButtonTriggerOff -= Deactivated;
        GameEvents.currentInstance.OnPreasureplateTriggerOn -= Activated;
        GameEvents.currentInstance.OnPreasureplateTriggerOff -= Deactivated;
        GameEvents.currentInstance.OnTriggerOn -= Activated;
        GameEvents.currentInstance.OnTriggerOff -= Deactivated;
    }
}
