using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour
{
    //Isak, Danny and Ai
    //When an object is inside the trigger collider a upwards force is applied.

    void OnTriggerStay(Collider other)
    {
        other.attachedRigidbody.AddForce(transform.up * 75, ForceMode.Acceleration);
        float y = 0;
        y = Mathf.Clamp(other.attachedRigidbody.velocity.y, -10000, 15);
        other.attachedRigidbody.velocity = new Vector3(other.attachedRigidbody.velocity.x, y, other.attachedRigidbody.velocity.z);
    }
}
