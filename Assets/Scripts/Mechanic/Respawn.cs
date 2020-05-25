using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    float respawnTriggerHeight = -120;

    Rigidbody playerRigidbody;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < respawnTriggerHeight)
            TriggerRespawn();
    }

    public void TriggerRespawn()
    {
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
    }
}
