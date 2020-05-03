using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    GameObject destroyedVersion;

    [SerializeField]
    Vector3 newScale;

    public void Break()
    {
        Instantiate(destroyedVersion, transform.position, destroyedVersion.transform.rotation);
        Destroy(gameObject);
    }

    void Start()
    {
        destroyedVersion.transform.localScale = newScale;
    }

    //void OnMouseDown()
    //{
    //    Instantiate(destroyedVersion, transform.position, transform.rotation);
    //    Destroy(gameObject);
    //}

}
