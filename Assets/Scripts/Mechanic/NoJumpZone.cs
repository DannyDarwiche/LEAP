﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoJumpZone : MonoBehaviour
{
    float savedValue;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            savedValue = other.transform.GetComponent<MovingCharacter>().jumpHeight;
            other.transform.GetComponent<MovingCharacter>().jumpHeight = 0f;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.GetComponent<MovingCharacter>().jumpHeight = savedValue;
    }
}