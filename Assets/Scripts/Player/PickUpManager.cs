using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpManager : MonoBehaviour
{
    [SerializeField]
    private int rayLength = 10;
    [SerializeField]
    private LayerMask layerMaskInteractable;
    [SerializeField]
    private Image uiCrosshair;
    [SerializeField]
    float throwForce = 20;
    [SerializeField]
    GameObject holdPos;
    [SerializeField]
    MovingCharacter player;

    Drop itemDropScript;
    GameObject raycastedObject;
    GameObject heldItem;
    Rigidbody heldItemRigidbody;
    bool isHolding = false;

    

    private void Update()
    {
        if (player.throwPickable)
            player.throwPickable = false;

        if ((Input.GetMouseButtonDown(0) && isHolding))
            DropItem();
        else if (Input.GetMouseButtonDown(1) && isHolding)
            DropItem(true);
        else if (isHolding == false)
            Raycast();
    }

    void Raycast()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteractable.value))
        {
            if (hit.collider.CompareTag("Pickable") || hit.collider.CompareTag("Torch") || hit.collider.CompareTag("KeyGreen") || hit.collider.CompareTag("KeyRed") || hit.collider.CompareTag("KeyBlue"))
            {
                raycastedObject = hit.collider.gameObject;
                CrosshairActive();

                if (Input.GetMouseButtonDown(0))
                    PickUp();
            }
        }
        else
            CrosshairNormal();
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

            heldItemRigidbody.angularVelocity = Vector3.zero;

            float distance = Vector3.Distance(heldItem.transform.position, holdPos.transform.position);
            if (distance > 2)
            {
                DropItem();
                return;
            }

            Vector3 movementLerp = Vector3.Lerp(heldItem.transform.position, holdPos.transform.position, Time.deltaTime * 10);
            heldItemRigidbody.MovePosition(movementLerp);
        }
    }

    void PickUp()
    {
        isHolding = true;
        heldItem = raycastedObject.gameObject;
        heldItemRigidbody = heldItem.GetComponent<Rigidbody>();
        heldItemRigidbody.useGravity = false;
        heldItemRigidbody.velocity = Vector3.zero;
        itemDropScript = heldItem.GetComponent<Drop>();
        itemDropScript.enabled = true;
        player.pickUp = true; 
    }

    public void DropItem(bool thrown = false)
    {
        isHolding = false;
        heldItemRigidbody.useGravity = true;
        heldItemRigidbody.velocity = Vector3.zero;
        if (thrown)
        {
            heldItemRigidbody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
            //player.throwPickable = true; 
        }    
        itemDropScript.enabled = false;
        itemDropScript.dropitem = false;
        itemDropScript = null;
        heldItem = null;
        player.pickUp = false; 
    }

    //void ThrowItem()
    //{
    //    heldItemRigidbody.useGravity = true;
    //    heldItemRigidbody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
    //    itemDropScript.enabled = false;
    //    itemDropScript.dropitem = false;
    //    itemDropScript = null;
    //    heldItem = null;
    //    isHolding = false;
    //}

    void CrosshairActive()
    {
        uiCrosshair.color = Color.red;
    }

    void CrosshairNormal()
    {
        uiCrosshair.color = Color.white;
    }
}
