using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BodyPart
{
    head,
    torso,
    arm,
    legs
}

public class AttachPart : MonoBehaviour
{
    [SerializeField]
    BodyPart bodyPart;

    [SerializeField]
    float breakForce;

    [SerializeField]
    bool rightArm;

    static List<GameObject> attachedPartsList;
    Vector3 offset;
    Vector3 anchor;
    ConfigurableJoint joint;
    SoftJointLimit softJointLimit;
    JointDrive jointDrive;
    bool jointBroken;

    void Start()
    {
        attachedPartsList = new List<GameObject>();

        joint = GetComponent<ConfigurableJoint>();
        joint.breakForce = breakForce;
        softJointLimit.limit = 1f;
        jointDrive.positionSpring = 10000f;
        jointDrive.positionDamper = 100f;
        jointDrive.maximumForce = float.PositiveInfinity;
        joint.xDrive = joint.yDrive = joint.zDrive = jointDrive;
    }

    void OnTriggerEnter(Collider other)
    {
        if (bodyPart.ToString() != other.tag.ToLower() || attachedPartsList.Contains(other.gameObject))
            return;

        switch (bodyPart)
        {
            case BodyPart.head:
                anchor = new Vector3(0, 0.5f, 0);
                offset = Vector3.zero;
                break;
            case BodyPart.torso:
                anchor = Vector3.zero;
                offset = Vector3.zero;
                break;
            case BodyPart.arm:
                anchor = new Vector3(0, 0.5f, 0);
                //float negativeFactor = 0f;
                //if (rightArm)
                //    negativeFactor = -1f;
                //else
                //    negativeFactor = 1f;
                offset = new Vector3(0, -0.5f, 0);
                break;
            case BodyPart.legs:
                anchor = new Vector3(0, 0.5f, 0);
                offset = Vector3.zero;
                break;
            default:
                break;
        }

        if (jointBroken)
        {
            joint = gameObject.AddComponent<ConfigurableJoint>();
            joint.xMotion = ConfigurableJointMotion.Limited;
            joint.yMotion = ConfigurableJointMotion.Limited;
            joint.zMotion = ConfigurableJointMotion.Limited;
            joint.axis = Vector3.one;
            joint.breakForce = breakForce;

            joint.anchor = anchor; //needs to change to be at connection point i.e neck, shoulders and hips
            joint.linearLimit = softJointLimit;
            joint.xDrive = joint.yDrive = joint.zDrive = jointDrive;

            jointBroken = false;
        }


        if (joint.connectedBody != null)
            return;

        other.transform.position = transform.position + offset;

        //Quaternion rotation = Quaternion.Euler(0, 0, 0);
        other.transform.rotation = transform.rotation;
        joint.connectedBody = other.attachedRigidbody;
        attachedPartsList.Add(other.gameObject);
        //joint.autoConfigureConnectedAnchor = false;
        //joint.connectedAnchor = Vector3.zero;
    }

    void OnJointBreak(float breakForce)
    {
        Debug.Log("From Attach Part: " + breakForce);
        GameObject removedPart = joint.connectedBody.gameObject;
        attachedPartsList.Remove(removedPart);
        jointBroken = true;
    }
}
