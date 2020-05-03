using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    int id; 

    bool activated; 
    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform != transform)
        {
            if (!activated)
            {
                GameEvents.currentInstance.ButtonTriggerOn(id);
                activated = true;
            }
            else
            {
                GameEvents.currentInstance.ButtonTriggerOff(id);
                activated = false;
            }
        }
    }

    void OnMouseDown()
    {
        if (!activated)
        {
            GameEvents.currentInstance.ButtonTriggerOn(id);
            activated = true;
        }
        else
        {
            GameEvents.currentInstance.ButtonTriggerOff(id);
            activated = false; 
        }
    }
}
