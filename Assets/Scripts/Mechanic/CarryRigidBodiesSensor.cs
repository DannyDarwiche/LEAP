using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryRigidBodiesSensor : MonoBehaviour
{
    [HideInInspector]
    public CarryRigidbodies carrier;
    [HideInInspector]
    public List<Rigidbody> rigidbodyList = new List<Rigidbody>();

    void OnTriggerEnter(Collider other)
    {
        Rigidbody body = other.attachedRigidbody;
        if (body != null && carrier.isActiveAndEnabled)
        {
            if (!rigidbodyList.Contains(body))
                rigidbodyList.Add(body);
            carrier.Add(body);
        }
    }
    void OnTriggerExit(Collider other)
    {
        Rigidbody body = other.attachedRigidbody;
        if (body != null)
        {
            if (rigidbodyList.Contains(body))
                rigidbodyList.Remove(body);
            carrier.TryRemoveBasedBySensors(body);
            Debug.Log("Test");
        }
    }
}
