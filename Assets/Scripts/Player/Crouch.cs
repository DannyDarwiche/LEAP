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
        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //    playerCollider.height = playerCollider.height / 2;

        //if (Input.GetKeyUp(KeyCode.LeftControl))
        //    playerCollider.height = playerCollider.height * 2;

        if (Input.GetKeyDown(KeyCode.LeftControl))
           gameObject.transform.localScale = new Vector3(1,0.5f,1);

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            gameObject.transform.position += Vector3.up / 2;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
