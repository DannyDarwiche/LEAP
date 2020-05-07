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
        GameObject gameObject = Instantiate(destroyedVersion, transform.position, destroyedVersion.transform.rotation);
        gameObject.transform.localScale = newScale;
        Destroy(gameObject);
    }
}
