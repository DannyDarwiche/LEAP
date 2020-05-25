using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatformEvent : MonoBehaviour
{
    [SerializeField]
    int id;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Player"))
            GameEvents.currentInstance.PlatformTriggerOn(id);
        //else
        //    GameEvents.currentInstance.PlatformTriggerOff(id);
    }
}
