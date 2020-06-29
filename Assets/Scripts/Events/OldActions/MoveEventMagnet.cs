using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEventMagnet : MonoBehaviour
{
    //Isak
    //Moves object in a certain direction activated by button.

    [SerializeField]
    float id;
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
    
    void Activated(float id)
    {
        if (id == this.id)
            active = true;
    }

    void Deactivated(float id)
    {
        if (id == this.id)
            active = false;
    }
}
