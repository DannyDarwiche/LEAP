using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    //Isak, Danny, Ai, Muhammad and Alexander
    //Activates an event when a button is pressed or collided with.

    [SerializeField]
    float id;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float pressDistance;

    bool activated; 

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
        if (Vector3.Distance(player.transform.position, transform.position) <= pressDistance)
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
}
