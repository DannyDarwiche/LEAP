using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destructible))]
public class BreakPoint : MonoBehaviour
{
    [SerializeField]
    Destructible destructible;

    [SerializeField, Range(0, 500)]
    float breakForce;

    void OnCollisionEnter(Collision collision)
    {
        Vector3 impactForce = collision.impulse;
        //Vector3 impactForce = Vector3.Scale(collision.impulse, transform.forward);
        Debug.Log(impactForce.magnitude);
        if (impactForce.magnitude > breakForce)
            destructible.Break();
    }
}
