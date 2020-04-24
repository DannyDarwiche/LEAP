using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpManager : MonoBehaviour
{
    private GameObject raycastedObject;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteractable;
    [SerializeField] private Image uiCrosshair;

    //Can be split into seperate pickup/throw script

    float throwForce = 20;
    Vector3 objectPos;
    float distance;

    private bool canHold = true;
    private GameObject heldItem;
    public GameObject holdPos;
    //public Collider itemCollider;
    private bool isHolding = false;
    //public Camera cam;

    private void Update()
    {
        //if (isHolding)
        //{
        //    heldItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    heldItem.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //    //heldItem.transform.position = holdPos.transform.position;
        //}



        if (Input.GetMouseButtonDown(0) && isHolding)
        {
            DropItem();
        }
        else if (Input.GetMouseButtonDown(1) && isHolding)
        {
            ThrowItem();
        }
        else if (isHolding == false)
            Raycast();
    }

    void Raycast()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteractable.value))
        {
            if (hit.collider.CompareTag("Pickable"))
            {
                raycastedObject = hit.collider.gameObject;
                CrosshairActive();

                if (Input.GetMouseButtonDown(0))
                {
                    PickUp();
                }
            }
        }
        else
        {
            CrosshairNormal();
        }
    }

    void PickUp()
    {
        isHolding = true;
        heldItem = raycastedObject.gameObject;
        heldItem.transform.position = holdPos.transform.position;
        heldItem.transform.parent = holdPos.transform;
        heldItem.GetComponent<Rigidbody>().isKinematic = true;
        heldItem.GetComponent<Collider>().enabled = false;
        //itemCollider.enabled = true;

        //isHolding = true;
        //heldItem = raycastedObject.gameObject;
        //heldItem.GetComponent<Rigidbody>().useGravity = false;
        //heldItem.GetComponent<Rigidbody>().detectCollisions = true;
        //heldItem.transform.position = holdPos.transform.position;
        //heldItem.transform.parent = holdPos.transform;
    }

    void DropItem()
    {
        //heldItem.transform.SetParent(null);
        //isHolding = false;
        //heldItem.GetComponent<Rigidbody>().useGravity = true;


        isHolding = false;
        heldItem.transform.SetParent(null);
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        heldItem.GetComponent<Collider>().enabled = true;
        //itemCollider.enabled = false;
        heldItem = null;
        isHolding = false;
    }

    void ThrowItem()
    {
        //    heldItem.transform.SetParent(null);
        //    isHolding = false;
        //    heldItem.GetComponent<Rigidbody>().useGravity = true;
        //    heldItem.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);


        heldItem.transform.SetParent(null);
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        heldItem.GetComponent<Collider>().enabled = true;
        //itemCollider.enabled = false;
        heldItem.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

        heldItem = null;
        isHolding = false;
    }

    void CrosshairActive()
    {
        uiCrosshair.color = Color.red;
    }

    void CrosshairNormal()
    {
        uiCrosshair.color = Color.white;
    }
}
