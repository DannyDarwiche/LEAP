using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEventMagnet : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    Vector3 moveDirection;
    [SerializeField]
    float moveSpeed;

    bool active;

    void Start()
    {
        GameEvents.currentInstance.OnButtonTriggerOn += Activated;
        GameEvents.currentInstance.OnButtonTriggerOff += Deactivated;
    }

    void Update()
    {
        if (active)
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    
    void Activated(int id)
    {
        if (id == this.id)
            active = true;
    }

    void Deactivated(int id)
    {
        if (id == this.id)
            active = false;
    }
}
