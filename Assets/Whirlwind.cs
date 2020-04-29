using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour
{
    //void OnTriggerEnter(Collider other)
    //{
    //    Vector3 upForce = new Vector3(0, 10, 0);
    //    other.attachedRigidbody.AddForce(upForce, ForceMode.VelocityChange);
    //}

    void OnTriggerStay(Collider other)
    {
        //Vector3 upForce = new Vector3(0, 75, 0);
        float upForce = 75;
        other.attachedRigidbody.AddForce(transform.up * 75, ForceMode.Acceleration);
        float y = 0;
        y = Mathf.Clamp(other.attachedRigidbody.velocity.y, -10000, 15);
        other.attachedRigidbody.velocity = new Vector3(other.attachedRigidbody.velocity.x, y, other.attachedRigidbody.velocity.z);
        Debug.Log(other.attachedRigidbody.velocity);
    }
}
