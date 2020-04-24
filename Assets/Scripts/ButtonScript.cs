using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] linkedObjects;

    MoveEvent[] linkedEvents;
    bool activated;

    void Start()
    {
        linkedEvents = new MoveEvent[linkedObjects.Length];
        for (int i = 0; i < linkedObjects.Length; i++)
            linkedEvents[i] = linkedObjects[i].GetComponent<MoveEvent>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Pickable") || collision.collider.CompareTag("Player"))
        {
            if (!activated)
            {
                foreach (MoveEvent linkedEvent in linkedEvents)
                    linkedEvent.Activated();
                activated = true;
            }
            else
            {
                foreach (MoveEvent linkedEvent in linkedEvents)
                    linkedEvent.Deactivated();
                activated = false;
            }
        }
    }
    void OnMouseDown()
    {
        Debug.Log(activated);
        if (!activated)
        {
            foreach (MoveEvent linkedEvent in linkedEvents)
                linkedEvent.Activated();
            activated = true;
        }
        else
        {
            foreach (MoveEvent linkedEvent in linkedEvents)
                linkedEvent.Deactivated();
            activated = false;
        }
    }
}
