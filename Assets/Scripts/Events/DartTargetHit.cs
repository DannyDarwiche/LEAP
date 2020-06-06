using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTargetHit : MonoBehaviour
{
    //Alexander
    //Activates an event upon collision and plays an animation.

    [SerializeField]
    int id;
    [SerializeField]
    Animator animator; 

    bool activated = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform != transform)
        {
            if (!activated)
            {
                animator.Play("MoveCartoonDart");
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
