using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableEventPuzzle : MonoBehaviour
{
    [SerializeField]
    int id;

    Renderer objectRenderer;
    Collider objectCollider;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();
        GameEvents.currentInstance.OnPuzzleSolvedTrigger += Activated;

    }
    void Activated(int id)
    {
        if(id == this.id)
        {
            objectRenderer.enabled = false;
            objectCollider.enabled = false;
        }
    }
}
