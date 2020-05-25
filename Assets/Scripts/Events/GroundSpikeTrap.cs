using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpikeTrap : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Respawn>().TriggerRespawn();
            //print("works");
        }
    }

}
