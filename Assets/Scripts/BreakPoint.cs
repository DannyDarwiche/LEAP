using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPoint : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 500)
        {
            
        }
    }
}
