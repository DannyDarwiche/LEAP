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
        GameObject shatteredGameObject = Instantiate(destroyedVersion, transform.position, destroyedVersion.transform.rotation);
        shatteredGameObject.transform.localScale = newScale;
        Destroy(gameObject);
    }
}
