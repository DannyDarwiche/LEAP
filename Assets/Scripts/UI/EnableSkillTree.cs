using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSkillTree : MonoBehaviour
{
    //Isak and Danny
    //Open and closes the skill tree when Tab is pressed.

    [SerializeField]
    GameObject hoverPanel;

    [SerializeField]
    Animator animatorSkillTree;

    bool activeState = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && animatorSkillTree.GetCurrentAnimatorStateInfo(0).normalizedTime >= animatorSkillTree.GetCurrentAnimatorStateInfo(0).length)
        {
            activeState = !activeState;
            hoverPanel.SetActive(false);

            if (activeState)
            {
                animatorSkillTree.SetBool("Rewind", true);
                Cursor.lockState = CursorLockMode.None; //Confined is recommended for the build but doesn't work in the Editor.
            }
            else
            {
                animatorSkillTree.SetBool("Rewind", false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
