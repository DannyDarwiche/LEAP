using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    //Isak, Ai and Danny
    //When a held item collides with enough force, the item is dropped.

    public bool dropitem;

    [SerializeField, Range(0, 50)]
    float dropForce;

    Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Player"))
            return;

        if (collision.relativeVelocity.magnitude > dropForce)
            dropitem = true;

        if (collision.impulse.magnitude > dropForce)
            dropitem = true;
    }
}
