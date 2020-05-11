using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    CapsuleCollider playerCollider;

    void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            playerCollider.height = playerCollider.height / 2;

        if (Input.GetKeyUp(KeyCode.LeftControl))
            playerCollider.height = playerCollider.height * 2;
    }
}
