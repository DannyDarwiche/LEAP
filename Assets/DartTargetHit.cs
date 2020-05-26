using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTargetHit : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    Animator anim; 

    bool activated = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform != transform)
        {
            if (!activated)
            {
                anim.Play("MoveCartoonDart");
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
