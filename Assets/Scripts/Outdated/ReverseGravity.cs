using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    bool reverseGrav = false;

    Quaternion startRot, endRot;
    public float rotSpeed;
    bool rotate = false;
    float counter = 0;

    float rotation;

    public GameObject player;
    Rigidbody rbPlayer;

    Vector3 revGrav = -Physics.gravity;


    //private void Awake()
    //{
    //    player = GameObject.Find("Player");

    //startRot = Quaternion.Euler(player.transform.rotation.eulerAngles.x,
    //                                   player.transform.rotation.eulerAngles.y,
    //                                   player.transform.rotation.eulerAngles.z);

    //    endRot = Quaternion.Euler(player.transform.rotation.eulerAngles.x,
    //        player.transform.rotation.eulerAngles.y,
    //        180);
    //}

    private void Awake()
    {
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rotate)
        {
            counter += Time.deltaTime;
            player.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, Mathf.Lerp(player.transform.rotation.eulerAngles.z, rotation, counter / rotSpeed));

            if (counter >= rotSpeed /*&& player.transform.rotation.z <= rotation + 1 && player.transform.rotation.z >= rotation - 1*/)
            {
                rotate = false;
                player.GetComponent<PlayerController>().rotating = false;
                player.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, rotation);
            }
        }

        if (reverseGrav)
        {
            rbPlayer.AddForce(revGrav);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            if (GameObject.Find("Player") == other.gameObject)
            {
                player = other.gameObject;
                player.GetComponent<PlayerController>().rotating = true;
                player.GetComponent<PlayerController>().jumpForce *= -1;

                rotation = 180;

                reverseGrav = true;
                counter = 0;
                rotate = true;

                //other.transform.rotation = Quaternion.Euler(other.transform.rotation.eulerAngles.x,
                //    other.transform.rotation.eulerAngles.y,
                //    180);

                //startRot = Quaternion.Euler(other.transform.rotation.eulerAngles.x,
                //                    other.transform.rotation.eulerAngles.y,
                //                    other.transform.rotation.eulerAngles.z);

                //endRot = Quaternion.Euler(other.transform.rotation.eulerAngles.x,
                //    other.transform.rotation.eulerAngles.y,
                //    -180);

                //player.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 180);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().useGravity = true;
            if (GameObject.Find("Player") == other.gameObject)
            {
                //player = other.gameObject;
                player.GetComponent<PlayerController>().rotating = true;
                player.GetComponent<PlayerController>().jumpForce *= -1;

                rotation = 0;

                reverseGrav = false;
                counter = 0;
                rotate = true;

                //startRot = Quaternion.Euler(other.transform.rotation.eulerAngles.x,
                //    other.transform.rotation.eulerAngles.y,
                //    other.transform.rotation.eulerAngles.z);

                //endRot = Quaternion.Euler(other.transform.rotation.eulerAngles.x,
                //    other.transform.rotation.eulerAngles.y,
                //    0);


                //player.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, 0);

                //player = null;
                //other.transform.rotation = Quaternion.Euler(other.transform.rotation.eulerAngles.x,
                //   other.transform.rotation.eulerAngles.y,
                //   0);
            }
        }
    }
}
