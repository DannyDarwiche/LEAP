using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWallSpike : MonoBehaviour
{
    //Alexander
    //Plays spike animation on object.

    [SerializeField]
    float id;
    [SerializeField]
    Animator animator;

    void Update()
    {
        Activated(id);
    }

    void Activated(float id)
    {
        if (id == this.id)
            animator.Play("SpikeAnimation");
    }
}
