    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGrabEvent : MonoBehaviour
{
    //Isak
    //When activated by a button it enables the object to pick up other objects like a magnet.

    [SerializeField]
    int id;
    [SerializeField]
    CarryRigidBodiesSensor sensor;
    [SerializeField]
    Collider sensorArea;

    List<Rigidbody> grabbedBodyList = new List<Rigidbody>();
    Renderer grabRenderer;
    bool noGravity;

    void Start()
    {
        grabRenderer = GetComponent<MeshRenderer>();
        GameEvents.currentInstance.OnButtonTriggerOn += Activated;
        GameEvents.currentInstance.OnButtonTriggerOff += Deactivated;
    }

    void Update()
    {
        if (!noGravity && grabbedBodyList.Count > 0)
            foreach (Rigidbody body in grabbedBodyList)
                body.useGravity = true;
    }

    void Activated(int id)
    {
        if (id == this.id)
        {
            sensorArea.enabled = true;
            sensor.enabled = true;
            grabRenderer.enabled = true;
            noGravity = true;
            foreach (Rigidbody rigidbody in grabbedBodyList)
                rigidbody.useGravity = true;
            grabbedBodyList.Clear();
        }
    }

    void Deactivated(int id)
    {
        if (id == this.id)
        {
            sensor.enabled = false;
            sensorArea.enabled = false;
            grabRenderer.enabled = false;
            noGravity = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (noGravity)
        {
            grabbedBodyList.Add(other.attachedRigidbody);
            other.attachedRigidbody.useGravity = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (noGravity)
        {
            grabbedBodyList.Remove(other.attachedRigidbody);
            other.attachedRigidbody.useGravity = true;
        }
    }
}
