using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crush : MonoBehaviour
{
    bool onRoof;
    bool onGround;

    void Die()
    {
        Debug.Log("Dead!");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        if (other.gameObject.CompareTag("Player"))
        {
            onGround = true;
            if (onRoof)
                Die();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            onGround = false;
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter");
        if (collision.gameObject.CompareTag("Player"))
        {
            onRoof = true;
            if (onGround)
                Die();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            onRoof = false;
    }
}
