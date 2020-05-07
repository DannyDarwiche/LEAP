using JetBrains.Annotations;
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

    [SerializeField]
    float throwForce = 20;
    Vector3 objectPos;
    float distance;
    Drop itemDropScript;

    private bool canHold = true;
    private GameObject heldItem;
    public GameObject holdPos;
    //public Collider itemCollider;
    private bool isHolding = false;
    //public Camera cam;

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) && isHolding))
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
            if (hit.collider.CompareTag("Pickable") || hit.collider.CompareTag("Torch"))
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

    void FixedUpdate()
    {
        if (isHolding)
        {
            if (itemDropScript.dropitem)
            {

                DropItem();
                
                return;
            }

            heldItem.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            float distance = Vector3.Distance(heldItem.transform.position, holdPos.transform.position);
            if (distance > 2)
            {

                DropItem();
                return;
            }

            Vector3 movementLerp = Vector3.Lerp(heldItem.transform.position, holdPos.transform.position, Time.deltaTime*10);
            
            heldItem.GetComponent<Rigidbody>().MovePosition(movementLerp);
        }
    }
    void PickUp()
    {
        isHolding = true;
        heldItem = raycastedObject.gameObject;
        heldItem.GetComponent<Rigidbody>().useGravity = false;
        itemDropScript = heldItem.GetComponent<Drop>();
        itemDropScript.enabled = true;
    }

    public void DropItem()
    {
        isHolding = false;
        heldItem.GetComponent<Rigidbody>().useGravity = true;
        itemDropScript.enabled = false;
        itemDropScript.dropitem = false;
        itemDropScript = null;
        heldItem = null;
        isHolding = false;
    }

    void ThrowItem()
    {
        heldItem.GetComponent<Rigidbody>().useGravity = true;
        heldItem.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
        itemDropScript.enabled = false;
        itemDropScript.dropitem = false;
        itemDropScript = null;
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
