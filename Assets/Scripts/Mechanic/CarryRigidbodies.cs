using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class CarryRigidbodies : MonoBehaviour
{
    [SerializeField]
    bool useSensors;

    List<Rigidbody> rigidbodyList = new List<Rigidbody>();
    List<CarryRigidBodiesSensor> sensorList = new List<CarryRigidBodiesSensor>();

    Vector3 lastEulerAngles;
    Vector3 lastPosition;

    public void Add(Rigidbody rb)
    {
        if (!rigidbodyList.Contains(rb))
            rigidbodyList.Add(rb);
    }

    public void Remove(Rigidbody rb)
    {
        if (rigidbodyList.Contains(rb))
            rigidbodyList.Remove(rb);
    }

    public bool TryRemoveBasedBySensors(Rigidbody body)
    {
        for (int i = 0; i < sensorList.Count; i++)
        {
            CarryRigidBodiesSensor sensor = sensorList[i];
            if (sensor.rigidbodyList.Contains(body))
                return false;
        }
        Remove(body);
        return true;
    }

    void Start()
    {
        lastPosition = transform.position;
        lastEulerAngles = transform.eulerAngles;
        if (useSensors)
        {
            foreach (CarryRigidBodiesSensor sensor in GetComponentsInChildren<CarryRigidBodiesSensor>())
            {
                sensor.carrier = this;
                sensorList.Add(sensor);
            }
            if (sensorList.Count == 0)
                Debug.LogError("You selected useSensors but you dont have any sensors in the game object");
        }
    }

    void LateUpdate()
    {
        if (rigidbodyList.Count > 0)
        {
            Vector3 velocity = transform.position - lastPosition;
            Vector3 angularVelocity = transform.eulerAngles - lastEulerAngles;
            for (int i = 0; i < rigidbodyList.Count; i++)
            {
                Rigidbody body = rigidbodyList[i];
                body.MovePosition(body.position + velocity);
                RotateRigidbody(body, angularVelocity);
            }
        }
        lastPosition = transform.position;
        lastEulerAngles = transform.eulerAngles;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (useSensors)
            return;
        Rigidbody body = collision.collider.attachedRigidbody;
        if (body != null)
            Add(body);
    }

    void OnCollisionExit(Collision collision)
    {
        if (useSensors)
            return;

        Rigidbody body = collision.collider.attachedRigidbody;
        if (body != null)
            Remove(body);
    }

    void RotateRigidbody(Rigidbody body, Vector3 angularVelocity)
    {
        Quaternion rotationQuaternion = Quaternion.Euler(angularVelocity);
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(rotationQuaternion);
        Vector3 newPosition = rotationMatrix.MultiplyVector(body.position - transform.position);
        body.MovePosition(newPosition + transform.position);
    }
}
