using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    //Danny
    //When a torch enters the fuse's trigger collider, a cannonball is fired.

    [SerializeField]
    GameObject cannonBallPrefab;
    [SerializeField]
    Transform cannonBallSpawnPosition;
    [SerializeField]
    float shootForce = 50;

    GameObject cannonBall;
    Rigidbody cannonBallRigidbody;

    void Awake()
    {
        cannonBall = Instantiate(cannonBallPrefab, cannonBallSpawnPosition);
        cannonBallRigidbody = cannonBall.GetComponent<Rigidbody>();
        cannonBall.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Torch"))
        {
            if (cannonBall.activeSelf == false)
                cannonBall.SetActive(true);

            cannonBall.transform.SetPositionAndRotation(cannonBallSpawnPosition.position, cannonBallSpawnPosition.rotation);
            cannonBallRigidbody.velocity = Vector3.zero;
            cannonBallRigidbody.angularVelocity = Vector3.zero;
            cannonBallRigidbody.AddForce(cannonBallSpawnPosition.forward * shootForce, ForceMode.Impulse);          
        }
    }
}
