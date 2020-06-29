using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatformEvent : MonoBehaviour
{
    //Alexander

    [SerializeField]
    float id;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Player"))
            GameEvents.currentInstance.PlatformTriggerOn(id);
    }
}
