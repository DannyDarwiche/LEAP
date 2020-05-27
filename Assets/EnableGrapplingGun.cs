using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGrapplingGun : MonoBehaviour
{
    [SerializeField]
    GameObject grapplingGun;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            grapplingGun.SetActive(!grapplingGun.activeSelf);
        }
    }
}
