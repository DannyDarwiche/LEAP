﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField]
    Rigidbody body;
    [SerializeField]
    float depthBeforeSubmerged = 1f, displacementAmount = 3f;
    [SerializeField]
    int floaterCount = 1;

    [SerializeField]
    float waterDrag = 0.99f, waterAngularDrag = 0.5f;

    void FixedUpdate()
    {
        //float waveHeight = WaveManager.instance.GetWaveHeight(transform.position.x);
        body.AddForceAtPosition(Physics.gravity/floaterCount, transform.position, ForceMode.Acceleration);
        if(transform.position.y < 0)
        {
            float displacementMultiplier = Mathf.Clamp01((-transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            body.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),transform.position, ForceMode.Acceleration);
            body.AddForce(displacementMultiplier * -body.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            body.AddTorque(displacementMultiplier * -body.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}