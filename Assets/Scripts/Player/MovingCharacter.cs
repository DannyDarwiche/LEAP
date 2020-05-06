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

        //With Update and FixedUpdate not always being in sync |= will guarantee that the input is never lost
        desiredJump |= Input.GetButtonDown("Jump") && PlayerStats.jump;
    }
    /*
     * FixedUpdate should be responsible for alll physics calculations and changes for the script
     */
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

            // The code saved all ground contacts we hit in a frame so to keep it as an unit vector, the contactNormal needs to be normalized if the groundContact is larger than 0
            if (groundContactCount > 1)
                contactNormal.Normalize();
        }
        else
        {
            contactNormal = Vector3.up;
        }
    }
    /*
     *  Jump is responsible for calculating how the character would jump after a jump has been decided.
     *  First it checks what type of jump it is using and then it calculates its force
     */
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
            jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
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
    /* 
     * To make it so that you cant wall jump unless the powerup is aquired we need to check what angle all the collision the character currently has are.
     * EvaluateCollision takes in all collision the character has had this frame and checks their normal with the ground and steep angles before saving them in their respective variable
     */
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
    /*
     * AdjustVelocity changes the angle of the movement compared to the ground normal.
     * Instead of forcing unity collision from pushing the character upwards a slope the characters forward movement will be angled with the slope instead
     */
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

    /*
     * ProjectOnContactPlane gets the movement vector on the ground by projecting in on the contact normal
     */
    Vector3 ProjectOnContactPlane(Vector3 vector)
    {
        return vector - contactNormal * Vector3.Dot(vector, contactNormal);
    }
    /*
     *  SnapToGround makes sure that when the character goes over the edge of a slope the characters upwards velocity will not make the character float.
     *  Instead snap back down to the ground unless a jump was made
     */
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
    /*
     *  GetMinDot prevents the SnapToGround to snap to anything with above the min angle for ground and stairs
     */
    float GetMinDot(int layer)
    {
        return (stairsMask & (1 << layer)) == 0 ? minGroundDotProduct : minStairsDotProduct;
    }
    /*
     * CheckSteepContacts prevents the character from getting stuck in a crevasses.
     * If they angle of all sides combined in the crevasse is within the allowed ground angle then that is also counted as ground
     */
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
}
