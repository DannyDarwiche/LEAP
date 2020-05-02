using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreasurePlate : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    float expectedMass;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    bool offOrOn;

    float currentMass;


    void OnTriggerEnter(Collider other)
    {
        if ((layerMask == (layerMask | (1 << other.gameObject.layer))) || other.CompareTag("Player"))
        {
            if (other.attachedRigidbody == null)
                return;

            currentMass += other.attachedRigidbody.mass;
            float percentage = currentMass / expectedMass;
            float activePercentage = Mathf.Clamp(percentage,0,1);
            GameEvents.currentInstance.PreasureplateTriggerOn(id,activePercentage);
            Debug.Log(currentMass);
        }
    }

    void OnTriggerExit(Collider other)
    {
        currentMass -= other.attachedRigidbody.mass;
        GameEvents.currentInstance.PreasureplatTriggerOff(id, currentMass / expectedMass);
        Debug.Log(currentMass);
    }
}
