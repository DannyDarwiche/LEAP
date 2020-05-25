using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSkillTree : MonoBehaviour
{
    [SerializeField]
    GameObject skillTree;

    [SerializeField]
    GameObject tokenImage;

    [SerializeField]
    GameObject hoverPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            skillTree.SetActive(!skillTree.activeSelf);
            tokenImage.SetActive(!tokenImage.activeSelf);
            hoverPanel.SetActive(false);
            if (skillTree.activeSelf == true)
                Cursor.lockState = CursorLockMode.None; //Confined is recommended for the build but doesn't work in the Editor.
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
