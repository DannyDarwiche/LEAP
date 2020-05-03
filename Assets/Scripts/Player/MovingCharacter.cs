using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
    public bool Grappling;

    [SerializeField]
    AudioSource audioStep;

    [SerializeField]
    AudioSource audioJump;

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f, maxAirAccelertaion = 1f;
    [SerializeField, Range(0f, 10f)]
    public float jumpHeight = PlayerStats.jumpHeight;
    [SerializeField, Range(0, 5)]
    int maxAirJumps = PlayerStats.maxAirJumps;
    [SerializeField, Range(0f, 90f)]
    float maxGroundAngle = 25f, maxStairsAngle = 50f;
    [SerializeField, Range(0f, 100f)]
    float maxSnapSpeed = 100f;

    //The distance of the raycast that determines when the player should snap to angled surfaces
    [SerializeField, Min(0f)]
    float probeDistance = 1f;

    //probeMask probes all layers to check if there's any object that count as a surface
    //except for layers that are put on Ignore Raycast and Agent
    [SerializeField]
    LayerMask probeMask = -1, stairsMask = -1;

    //velocity is used to override the velocity of the Rigidbody component
    //desiredVelocity is the velocity after having the acceleration applied to reach a smoother player motion
    Vector3 velocity, desiredVelocity, contactNormal, steepNormal;

    Rigidbody body;

    bool desiredJump;
    bool sprint;
    float maxSprintSpeed;

    int jumpPhase, groundContactCount, steepContactCount;

    //minGroundDotProduct describes the maximum angle you can climb on a plain surface
    //minStairsDotProduct describes the maximum angle you can climb on stairs
    float minGroundDotProduct, minStairsDotProduct;

    int stepsSinceLastGrounded, stepsSinceLastJump;
   
    bool OnGround => groundContactCount > 0;
    bool OnSteep => steepContactCount > 0;
    void OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
    }
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        OnValidate();
        maxSprintSpeed = maxSpeed * 2;
    }
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        sprint = Input.GetButton("Sprint") && PlayerStats.sprint;
        float speed = sprint ? maxSprintSpeed : maxSpeed;
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * speed;
        StepAudio();
        AudioJump();
        desiredJump |= Input.GetButtonDown("Jump") && PlayerStats.jump;
    }
    void FixedUpdate()
    {
        UpdateState();
        AdjustVelocity();

        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }
        body.velocity = velocity;
        ClearState();
    }

    void ClearState()
    {
        groundContactCount = steepContactCount = 0;
        contactNormal = steepNormal = Vector3.zero;
        
    }

    void UpdateState()
    {
        stepsSinceLastGrounded += 1;
        stepsSinceLastJump += 1;
        velocity = body.velocity;
        if (OnGround || SnapToGround() || CheckSteepContacts())
        {
            stepsSinceLastGrounded = 0;
            if (stepsSinceLastJump > 1)
                jumpPhase = 0;
            if (groundContactCount > 1)
                contactNormal.Normalize();
        }
        else
        {
            contactNormal = Vector3.up;
        }
    }

    void Jump()
    {
        Vector3 jumpDirection;
        if (OnGround)
            jumpDirection = contactNormal;
        else if (OnSteep && PlayerStats.walljump)
        {
            jumpDirection = steepNormal;
            jumpPhase = 0;
        }
        else if (jumpPhase <= maxAirJumps)
        {
            if (jumpPhase == 0)
                jumpPhase = 1;
            jumpDirection = contactNormal;
        }
        else
            return;
        //velocity.y = 0;
        stepsSinceLastJump = 0;
        jumpPhase += 1;
        float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
        jumpDirection = (jumpDirection + Vector3.up).normalized;
        float alignedSpeed = Vector3.Dot(velocity, jumpDirection);

        if (alignedSpeed > 0f)
        {
            jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
        }
        velocity += jumpDirection * jumpSpeed;
    }
    void AudioJump()
    {
        if (Input.GetButtonDown("Jump") && !audioJump.isPlaying && OnGround)
        {
            audioJump.Play();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }
    void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }
    void EvaluateCollision(Collision collision)
    {
        float minDot = GetMinDot(collision.gameObject.layer);
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            if (normal.y >= minDot)
            {
                groundContactCount += 1;
                contactNormal += normal;
            }
            else if (normal.y > -0.01f)
            {
                steepContactCount += 1;
                steepNormal += normal;
            }
        }
    }
    void AdjustVelocity()
    {
        Vector3 xAxis = ProjectOnContactPlane(transform.right).normalized;
        Vector3 zAxis = ProjectOnContactPlane(transform.forward).normalized;

        float currentX = Vector3.Dot(velocity, xAxis);
        float currentZ = Vector3.Dot(velocity, zAxis);
        float accelertaion = OnGround ? maxAcceleration : maxAirAccelertaion;
        float maxSpeedChange = accelertaion * Time.deltaTime;

        if (Grappling)
        {
            desiredVelocity.x += currentX;
            desiredVelocity.z += currentZ;
        }
        float newX = Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
        float newZ = Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);

        velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }
    void StepAudio()
    {
        if (OnGround && desiredVelocity.magnitude > 0)
        {
            if (!audioStep.isPlaying)
            {
                audioStep.Play();
            }
        }
        else
            audioStep.Stop();
    }
    Vector3 ProjectOnContactPlane(Vector3 vector)
    {
        return vector - contactNormal * Vector3.Dot(vector, contactNormal);
    }
    bool SnapToGround()
    {
        if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2)
        {
            return false;
        }
        float speed = velocity.magnitude;
        if (speed > maxSnapSpeed)
            return false;
        if (!Physics.Raycast(body.position, Vector3.down, out RaycastHit hit, probeDistance, probeMask))
        {
            return false;
        }
        if (hit.normal.y < GetMinDot(hit.collider.gameObject.layer))
        {
            return false;
        }
        groundContactCount = 1;
        contactNormal = hit.normal;
        float dot = Vector3.Dot(velocity, hit.normal);
        if (dot > 0f)
        {
            velocity = (velocity - hit.normal * dot).normalized * speed;
        }
        return true;
    }
    float GetMinDot(int layer)
    {
        return (stairsMask & (1 << layer)) == 0 ? minGroundDotProduct : minStairsDotProduct;
    }

    bool CheckSteepContacts()
    {
        if (steepContactCount > 1)
        {
            steepNormal.Normalize();

            if (steepNormal.y >= minGroundDotProduct)
            {
                groundContactCount = 1;
                contactNormal = steepNormal;
                return true;
            }
        }
        return false;
    }
}
