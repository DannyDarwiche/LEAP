using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kinematicOn : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag != "Player")
            collision.collider.attachedRigidbody.isKinematic = true;


    }
}
