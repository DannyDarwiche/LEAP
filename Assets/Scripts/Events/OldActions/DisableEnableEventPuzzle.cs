using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableEventPuzzle : MonoBehaviour
{
    //Isak
    //Activates and deactivates based on the status of a certain puzzle.

    [SerializeField]
    float id;

    Renderer objectRenderer;
    Collider objectCollider;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();
        GameEvents.currentInstance.OnPuzzleSolvedTrigger += Activated;
        GameEvents.currentInstance.OnPuzzleFailedTrigger += Deactivated;
    }

    void Activated(float id)
    {
        if(id == this.id)
        {
            objectRenderer.enabled = false;
            objectCollider.enabled = false;
        }
    }

    void Deactivated(float id)
    {
        if(id == this.id)
        {
            objectRenderer.enabled = true;
            objectCollider.enabled = true;
        }
    }
}
