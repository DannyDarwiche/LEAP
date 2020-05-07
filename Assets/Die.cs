using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField]
    Rigidbody playerBody;
    [SerializeField]
    Transform spawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody == playerBody)
        {
            playerBody.velocity = Vector3.zero;
            playerBody.angularVelocity = Vector3.zero;
            transform.SetPositionAndRotation(spawnPoint.position,spawnPoint.rotation);
        }
    }
}
