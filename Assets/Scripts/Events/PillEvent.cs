using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillEvent : MonoBehaviour
{

    [SerializeField]
    int id;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float pressDistance;

    bool activated;


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
