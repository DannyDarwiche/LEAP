using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    bool isRed;

    public void MouseUp()
    {
        isRed = !isRed;
        if (isRed)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
            GetComponent<Renderer>().material.color = Color.white; 

    }
}
