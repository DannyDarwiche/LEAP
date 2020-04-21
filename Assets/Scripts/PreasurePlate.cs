using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreasurePlate : MonoBehaviour
{
    [SerializeField]
    GameObject[] linkedObject;

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

    float[] startY;

    void Start()
    {
        int index = 0;
        startY = new float[linkedObject.Length];
        foreach (GameObject lo in linkedObject)
        {
            startY[index] = lo.transform.position.y;
            index++;
        }
    }

    void Update()
    {
        int index = 0;

        float yLiftHeight = currentMass / expectedMass * liftHeight;
        float maxSpeedChange = liftSpeed * Time.deltaTime;

        foreach (GameObject lo in linkedObject)
        {
            float newY = Mathf.MoveTowards(lo.transform.position.y, startY[index] + yLiftHeight * lo.transform.up.y, maxSpeedChange);
            Vector3 moveDistance = new Vector3(lo.transform.position.x, newY, lo.transform.position.z);
            lo.transform.SetPositionAndRotation(moveDistance, lo.transform.rotation);
            index++;
        }
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
