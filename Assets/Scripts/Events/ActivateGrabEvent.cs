using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGrabEvent : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    CarryRigidbodies magnet;
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
        {
            foreach (Rigidbody body in grabbedBodyList)
            {
                body.useGravity = true;
            }
        }
    }
    void Activated(int id)
    {
        if (id == this.id)
        {
            magnet.enabled = true;
            grabRenderer.enabled = true;
            noGravity = true;
        }
    }
    void Deactivated(int id)
    {
        if (id == this.id)
        {
            magnet.enabled = false;
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
            grabbedBodyList.Add(other.attachedRigidbody);
            other.attachedRigidbody.useGravity = true;
        }
    }
}
