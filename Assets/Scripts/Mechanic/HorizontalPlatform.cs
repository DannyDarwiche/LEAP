using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        
        collision.collider.transform.SetParent(this.transform,true);
    }
    void OnCollisionExit(Collision collision)
    {
        collision.collider.transform.SetParent(null, true);
    }
}
