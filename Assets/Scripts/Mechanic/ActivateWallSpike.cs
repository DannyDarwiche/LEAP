using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWallSpike : MonoBehaviour
{
    //Alexander
    //Plays spike animation on object.

    [SerializeField]
    int id;
    [SerializeField]
    Animator animator;

    void Update()
    {
        Activated(id);
    }

    void Activated(int id)
    {
        if (id == this.id)
            animator.Play("SpikeAnimation");
    }
}
