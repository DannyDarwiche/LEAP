using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpikeTrap : MonoBehaviour
{
    //Alexander
    //Kills the player upon collision with spike.

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Respawn>().TriggerRespawn(false);
    }
}
