using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField]
    GameObject item;
    [SerializeField]
    GameObject tempParent;

    [SerializeField]
    float rayLength;
    [SerializeField]
    LayerMask layerMaskInteractable;

    [SerializeField]
    float anchorSpeed;

    [SerializeField]
    ConfigurableJoint joint;

    [SerializeField]
    float breakForce = 2000;

    Vector3 objectPos;
    Vector3 cursorPosition;
    bool heldItem, jointBroken;
    Vector3 offset;
    Vector3 offsetObjectSpace;
    Vector3 distanceToPlayer;

    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        joint.breakForce = breakForce;
        distanceToPlayer = transform.parent.parent.position - Camera.main.transform.position;
    }

    void Update()
    {
        if (jointBroken)
        {
            joint = gameObject.AddComponent<ConfigurableJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;
            joint.axis = Vector3.one;
            joint.breakForce = breakForce;

            jointBroken = false;
        }

        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToPlayer.z);
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
        transform.position = new Vector3(cursorPosition.x, cursorPosition.y, 0);

        if (joint == null)
            return;

        if (joint.connectedBody != null)
            joint.connectedAnchor = Vector3.MoveTowards(joint.connectedAnchor, offsetObjectSpace, Time.deltaTime * anchorSpeed);

        if (Input.GetMouseButtonDown(0))
        {
            if (!heldItem)
                Raycast();
        }

        if (Input.GetMouseButtonUp(0))
        {
            joint.connectedBody = null;
            heldItem = false;
        }

    }

    void Raycast()
    {
        RaycastHit hit;
        Vector3 fwd = cursorPosition - Camera.main.transform.position;
        if (Physics.Raycast(Camera.main.transform.position, fwd, out hit, rayLength, layerMaskInteractable.value))
        {

            heldItem = true;
            offset = hit.point - hit.transform.position;
            item = hit.collider.gameObject;
            offsetObjectSpace = item.transform.InverseTransformVector(hit.point - hit.transform.position);
            //item.transform.SetParent(tempParent.transform);
            joint.connectedBody = hit.collider.attachedRigidbody;
            joint.connectedAnchor = item.transform.InverseTransformVector((transform.position - item.transform.position));
            //item.transform.localPosition = Vector3.zero - offset;
            //item.GetComponent<Rigidbody>().useGravity = false;
            //item.GetComponent<Rigidbody>().detectCollisions = true;
            //item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;

            //StartCoroutine(resetVelocity());
        }
    }

    void OnJointBreak(float breakForce)
    {
        Debug.Log(breakForce);
        jointBroken = true;
        item.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    //IEnumerator resetVelocity()
    //{
    //    while (heldItem)
    //    {
    //        item.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //        yield return null;
    //    }

    //    //item.transform.SetParent(null);
    //    item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    //    item.GetComponent<Rigidbody>().useGravity = true;
    //}
}
