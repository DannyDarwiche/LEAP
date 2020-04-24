using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun2 : MonoBehaviour
{
    public string fireButton = "Fire1";
    public float grabDistance = 10.0f;
    public Transform holdPosition;
    public float throwForce = 100;
    public ForceMode throwForceMode;
    public LayerMask layerMask;

    GameObject heldObject = null;

    void Start()
    {

    }

    void Update()
    {
        if (heldObject == null)
        {
            if (Input.GetButtonDown(fireButton))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, layerMask))
                {
                    heldObject = hit.collider.gameObject;                   
                }
            }
        }
        else
        {
            heldObject.transform.position = holdPosition.position;
            heldObject.transform.rotation = holdPosition.rotation;

            if (Input.GetButtonDown(fireButton))
            {
                Rigidbody body = heldObject.GetComponent<Rigidbody>();
                body.isKinematic = false;
                heldObject.GetComponent<Collider>().enabled = true;
                body.AddForce(throwForce * transform.forward, throwForceMode);
                heldObject = null;
            }
        }

    }
    //Källa: https://www.youtube.com/watch?v=sxjJTNDmsug&list=PLfRfKfCcFo_sVdoFRqxUpjNyamd5P9NOm&index=4
}
