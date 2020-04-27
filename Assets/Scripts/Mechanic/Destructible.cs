using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;

    public void Break()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Start()
    {
        destroyedVersion.transform.localScale = this.transform.localScale/100;
    }

    //void OnMouseDown()
    //{
    //    Instantiate(destroyedVersion, transform.position, transform.rotation);
    //    Destroy(gameObject);
    //}

}
