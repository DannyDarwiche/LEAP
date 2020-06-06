using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMatch : MonoBehaviour
{
    //Isak
    //Check if an other object has the same color as this game object.

    [HideInInspector]
    public bool correctCube;

    [SerializeField]
    Renderer platformRenderer;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Renderer>().material.color == platformRenderer.material.color)
            correctCube = true;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Renderer>().material.color == platformRenderer.material.color)
            correctCube = false;
    }
}
