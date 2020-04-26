using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balancingBarrel : MonoBehaviour
{
    public bool started;

    [SerializeField]
    Rigidbody body;
    [SerializeField]
    Transform endPosition;

    Vector3 startPosition;
    Quaternion startRotation;
    bool addforce, done;

    void Start()
    {
        startPosition = body.position;
        startRotation = body.rotation;
    }
    void FixedUpdate()
    {
        if (started)
        {
            if (addforce)
            {
                body.AddTorque(transform.up, ForceMode.Acceleration);
            }
            else
            {
                Vector3 moveLerp = Vector3.Lerp(body.position, startPosition, Time.deltaTime);
                body.MovePosition(moveLerp);
                Quaternion rotationLerp = Quaternion.Lerp(body.rotation, startRotation, Time.deltaTime * 1000);
                body.MoveRotation(rotationLerp);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            addforce = true;

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            addforce = false;

    }
}
