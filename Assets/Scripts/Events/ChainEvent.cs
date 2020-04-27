using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChainEvent : MonoBehaviour
{
    [SerializeField]
    MoveEvent firstEvent;
    [SerializeField]
    SpawnEvent secondEvent;

    void Update()
    {
        if (firstEvent.activated)
            secondEvent.Activated();
        else
            secondEvent.Deactivated();
    }
}
