using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreasurePlateActivator : MonoBehaviour
{
    //Isak, Danny and Ai
    //Activates event based on mass on a preassure plate.

    [SerializeField]
    float id;
    [SerializeField]
    int expectedMass;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    bool offOrOn;

    int currentMass;

    void OnTriggerEnter(Collider other)
    {
        if ((layerMask == (layerMask | (1 << other.gameObject.layer))) || other.CompareTag("Player"))
        {
            if (other.attachedRigidbody == null)
                return;

            currentMass += (int)other.attachedRigidbody.mass;

            if (offOrOn)
            {
                GameEvents.currentInstance.PreasureplateTriggerOn(id, 1f);
                return;
            }

            float percentage = currentMass / expectedMass;
            float activePercentage = Mathf.Clamp(percentage, 0, 1);
            GameEvents.currentInstance.PreasureplateTriggerOn(id, activePercentage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        currentMass -= (int)other.attachedRigidbody.mass;

        if (offOrOn)
        {
            if (currentMass <= 0)
            {
                GameEvents.currentInstance.PreasureplateTriggerOff(id, 0f);
            }
            return;
        }

        GameEvents.currentInstance.PreasureplateTriggerOff(id, currentMass / expectedMass);
    }
}
