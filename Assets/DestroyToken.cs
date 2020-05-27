using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyToken : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(4f);

        Destroy(gameObject);
    }
}
