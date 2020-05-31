using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    Transform spawnPointHub;
    [SerializeField]
    Transform spawnPointCastle;
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
            TriggerRespawn(true);
    }

    public void TriggerRespawn(bool hub)
    {
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
        if (hub)
            transform.SetPositionAndRotation(spawnPointHub.position, spawnPointHub.rotation);
        else
            transform.SetPositionAndRotation(spawnPointCastle.position, spawnPointCastle.rotation);
    }
}
