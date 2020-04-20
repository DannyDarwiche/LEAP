using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreasurePlate : MonoBehaviour
{
    [SerializeField]
    GameObject linkedObject;

    [SerializeField]
    float expectedMass;

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    float liftSpeed;

    [SerializeField]
    float liftHeight;

    int layer;

    float currentMass;

    float startY; 

    void Start()
    {
        startY = linkedObject.transform.position.y;
    }

    void Update()
    {
        float yLiftHeight = currentMass / expectedMass * liftHeight;
        float maxSpeedChange = liftSpeed * Time.deltaTime;
        float newY = Mathf.MoveTowards(linkedObject.transform.position.y, startY + yLiftHeight, maxSpeedChange);
        Vector3 moveDistance = new Vector3(linkedObject.transform.position.x, newY, linkedObject.transform.position.z);
        linkedObject.transform.SetPositionAndRotation(moveDistance, linkedObject.transform.rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentMass == expectedMass)
            return;

        if ((layerMask == (layerMask | (1 << other.gameObject.layer))) || other.CompareTag("Player"))
        {
            if (other.attachedRigidbody == null)
                return;

            currentMass += other.attachedRigidbody.mass;
        }
    }

    void OnTriggerExit(Collider other)
    {
        currentMass -= other.attachedRigidbody.mass;
    }
}
