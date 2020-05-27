using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSkillTree : MonoBehaviour
{
    //[SerializeField]
    //GameObject skillTree;

    //[SerializeField]
    //GameObject tokenImage;

    [SerializeField]
    GameObject hoverPanel;

    [SerializeField]
    Animator animatorSkillTree;

    //[SerializeField]
    //Animator animatorTokenCount;

    bool activeState = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && animatorSkillTree.GetCurrentAnimatorStateInfo(0).normalizedTime >= animatorSkillTree.GetCurrentAnimatorStateInfo(0).length)
        {
            Debug.Log("Got in");
            activeState = !activeState;
            //skillTree.SetActive(true);
            //tokenImage.SetActive(!tokenImage.activeSelf);
            hoverPanel.SetActive(false);

            if (activeState)
            {
                animatorSkillTree.SetBool("Rewind", true);
                //animatorTokenCount.SetBool("Rewind", true);
                Cursor.lockState = CursorLockMode.None; //Confined is recommended for the build but doesn't work in the Editor.
            }
            else
            {
                animatorSkillTree.SetBool("Rewind", false);
                //animatorTokenCount.SetBool("Rewind", false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
