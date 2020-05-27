using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWallSpike : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    Animator animator;

    //bool activated; 

    void Activated(int id)
    {
        if (id == this.id)
            animator.Play("SpikeAnimation");
            
    }

    // Update is called once per frame
    void Update()
    {
        Activated(id);
    }
}
