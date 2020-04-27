using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreasurePlate : MonoBehaviour
{
    [SerializeField]
    float expectedMass;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    GameObject[] linkedObjects;
    [SerializeField]
    bool offOrOn;

    float currentMass;

    MoveEvent[] linkedEvents;
    void Start()
    {
        linkedEvents = new MoveEvent[linkedObjects.Length];

        for (int i = 0; i < linkedObjects.Length; i++)
        {

            linkedEvents[i] = linkedObjects[i].GetComponent<MoveEvent>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentMass == expectedMass)
            return;

        if ((layerMask == (layerMask | (1 << other.gameObject.layer))) || other.CompareTag("Player"))
        {
            if (other.attachedRigidbody == null)
                return;

            currentMass += other.attachedRigidbody.mass;
            Debug.Log(currentMass);
        }
    }

    void OnTriggerExit(Collider other)
    {
        currentMass -= other.attachedRigidbody.mass;
        Debug.Log(currentMass);
    }
    void Update()
    {
        if (offOrOn)
        {
            if(currentMass > 0)
                foreach (MoveEvent linkedevent in linkedEvents)
                    linkedevent.Activated();
            else
                foreach (MoveEvent linkedEvent in linkedEvents)
                    linkedEvent.Deactivated();

        }
        else if(currentMass > 0)
        {
            foreach (MoveEvent linkedEvent in linkedEvents)
                linkedEvent.Activated(currentMass / expectedMass);
        }
        else
            foreach (MoveEvent linkedEvent in linkedEvents)
                linkedEvent.Deactivated();
    }
}
