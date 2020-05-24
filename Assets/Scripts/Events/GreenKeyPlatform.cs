using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenKeyPlatform : MonoBehaviour
{

    [SerializeField]
    int id;

    bool gotTriggerd = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("KeyGreen"))
        {
            gotTriggerd = true;
        }
        else
        {
            gotTriggerd = false; 
        }
    }
    void Update()
    {
        if (gotTriggerd)
        {
            GameEvents.currentInstance.PlatformTriggerOn(id);
        }
        else if (!gotTriggerd)
            GameEvents.currentInstance.PlatformTriggerOff(id);
    }
}
