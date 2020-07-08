using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destructible))]
public class BreakPoint : MonoBehaviour
{
    //Isak
    //Activates when enough force is applied upon collision.

    [SerializeField]
    Destructible destructible;

    [SerializeField, Range(0, 500)]
    float breakForce;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.impulse.magnitude);

        Vector3 impactForce = collision.impulse;
        if (impactForce.magnitude > breakForce)
            destructible.Break();
    }
}
