using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    Animator animator;

    bool openNeg, openPos;
    void OnMouseDown()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if(distance < 10)
        {
            if (Vector3.Dot(this.transform.forward, player.transform.forward) < 0)
            {
                Debug.Log("Test");
                if (openPos || openNeg)
                {
                    animator.enabled = true;
                    animator.SetBool("OpenNeg", false);
                    openNeg = false;
                    animator.SetBool("OpenPos", false);
                    openPos = false;
                }
                else
                {
                    animator.SetBool("OpenNeg", true);
                    openNeg = true;
                }
            }
            else
            {
                Debug.Log("Test1");
                if (openNeg || openPos)
                {
                    animator.enabled = true;
                    animator.SetBool("OpenNeg", false);
                    openNeg = false;
                    animator.SetBool("OpenPos", false);
                    openPos = false;

                }
                else
                {
                    animator.SetBool("OpenPos", true);
                    openPos = true;
                }
            }
        }
    }
    void PauseAnimationEvent()
    {
        animator.enabled = false;
    }
}
