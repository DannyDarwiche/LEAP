using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    //Isak
    //Kills the player upon entering the lavas trigger.

    [SerializeField]
    Rigidbody playerBody;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    Transform startPosition;

    void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody == playerBody)
        {
            playerBody.velocity = Vector3.zero;
            playerBody.angularVelocity = Vector3.zero;
            playerBody.transform.SetPositionAndRotation(spawnPoint.position,spawnPoint.rotation);
        }
        else
        {
            other.attachedRigidbody.velocity = Vector3.zero;
            other.attachedRigidbody.angularVelocity = Vector3.zero;
            other.transform.SetPositionAndRotation(startPosition.position, startPosition.rotation);
        }
    }
}
