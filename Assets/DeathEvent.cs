using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.currentInstance.OnDeath += PlayerDeath;
        StartCoroutine(FellToDeath());
    }

    void PlayerDeath()
    {
        Debug.Log("Dead");
    }

    IEnumerator FellToDeath()
    {
        while (true)
        {
            if (transform.position.y < -80)
            {
                Debug.Log("Fell To Death");
                transform.position = Vector3.zero;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

            yield return new WaitForSeconds(5);
        }
    }

    void OnDestroy()
    {
        GameEvents.currentInstance.OnDeath -= PlayerDeath;
    }
}
