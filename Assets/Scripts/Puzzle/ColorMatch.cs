using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMatch : MonoBehaviour
{
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
