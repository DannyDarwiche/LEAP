using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(MovingCharacter))]
public class GraplingGun : MonoBehaviour
{
    [SerializeField]
    LayerMask whatIsGrappleable;

    [SerializeField]
    Transform firePoint, camera;

    [SerializeField]
    Transform player;

    [SerializeField]
    MovingCharacter movingCharacter;

    [SerializeField]
    float maxGrappleDistance = 8f;

    [SerializeField]
    float maxDistanceFactor = 0.8f;

    [SerializeField]
    float minDistanceFactor = 0.1f;

    [SerializeField]
    float spring = 900f;

    [SerializeField]
    float damper = 1400f;

    [SerializeField]
    float massScale = 20f;

    [SerializeField]
    float grappleCooldown = 2f;

    [SerializeField]
    float gunRotationSpeed = 5f;

    Rigidbody playerRigidBody;
    SpringJoint joint;
    LineRenderer lineRenderer;
    Quaternion desiredRotation;
    Vector3 grapplePoint;
    Vector3 currentGrapplePosition;
    float startSpring;
    float grappleTimer;
    bool pullingGrapple;
    bool grappling;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerRigidBody = player.GetComponent<Rigidbody>();
        startSpring = spring;
    }

    void Update()
    {
        RotateGun();

        if (grappleTimer >= 0f)
            grappleTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.B) && !Input.GetMouseButtonDown(0))
            pullingGrapple = !pullingGrapple;

        if (Input.GetMouseButtonDown(0) && grappleTimer <= 0f)
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0) && grappling)
        {
            StopGrapple();
        }

        if (pullingGrapple && joint != null)
        {
            spring = Mathf.MoveTowards(spring, startSpring, 5);
            joint.spring = spring;
        }
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grappling = true;

            movingCharacter.grappling = true;
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * maxDistanceFactor;
            joint.minDistance = distanceFromPoint * minDistanceFactor;

            if (!pullingGrapple)
            {
                Debug.Log("Normal");

                //playerRigidBody.AddForce(Vector3.down * 100, ForceMode.Impulse);

                //Adjust these values to fit your game.
                joint.spring = spring;
                joint.damper = damper;
                joint.massScale = massScale;
            }
            else
            {
                Debug.Log("Pulling");
                //Adjust these values to fit your game.
                joint.maxDistance = 0.4f;
                joint.minDistance = 0.2f;
                joint.spring = spring = spring * distanceFromPoint;
                joint.damper = damper / 2;
                joint.massScale = 20f;
            }

            lineRenderer.positionCount = 2;
            currentGrapplePosition = firePoint.position;
        }
    }

    void StopGrapple()
    {
        grappling = false;
        grappleTimer = grappleCooldown;
        spring = startSpring;
        movingCharacter.grappling = false;
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, currentGrapplePosition);
    }

    void RotateGun()
    {
        if (!grappling)
            desiredRotation = transform.parent.rotation;
        else
            desiredRotation = Quaternion.LookRotation(transform.position-grapplePoint);

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, gunRotationSpeed * Time.deltaTime);
    }

    //public bool IsGrappling()
    //{
    //    return joint != null;
    //}

    //public Vector3 GetGrapplePoint()
    //{
    //    return grapplePoint;
    //}
}