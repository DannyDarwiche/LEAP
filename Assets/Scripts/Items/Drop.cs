using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
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
      //  Debug.Log(body.velocity.magnitude);
        if (collision.relativeVelocity.magnitude > dropForce)
        {
            Debug.Log("Dropped");
            dropitem = true;
        }
        if (collision.impulse.magnitude > dropForce)
        {
            Debug.Log("Dropped");
            dropitem = true;
        }
    }
}
