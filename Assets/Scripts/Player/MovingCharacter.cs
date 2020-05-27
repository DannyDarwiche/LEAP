using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
    [HideInInspector]
    public bool grappling;

    [HideInInspector]
    public bool pickUp, throwPickable;

    [SerializeField]
    AudioSource audioStep;
    [SerializeField]
    AudioSource audioJump;

    [Header("Basic Movement")]
    [SerializeField, Range(0f, 100f)]
    public float maxSpeed = 6f;
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f, maxAirAccelertaion = 1f;
    [SerializeField, Range(0f, 10f)]
    public float jumpHeight = 2.5f;
    [SerializeField, Range(0, 5)]
    public int maxAirJumps = 2;
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

    [Header("Walljump")]
    [SerializeField]
    float wallJumpCooldown = 1f;

    [Header("Dash")]
    [SerializeField, Range(0f, 5f)]
    public int dashPhase = 1;
    [SerializeField, Range(0f, 100f)]
    float dashForce = 30f;
    [SerializeField, Min(0f)]
    public float dashDuration = 0.2f;
    [SerializeField, Range(0f, 3f)]
    public float dashCooldown = 1f;
    [SerializeField]
    float normalFov = 60f;
    [SerializeField]
    float dashFov = 100f;

    [Header("Grappling Gun")]
    [SerializeField]
    GraplingGun graplingGun;

    [Header("Animator")]
    [SerializeField]
    Animator animator;

    //velocity is used to override the velocity of the Rigidbody component
    //desiredVelocity is the velocity after having the acceleration applied to reach a smoother player motion
    Vector3 velocity, desiredVelocity, contactNormal, steepNormal;

    Rigidbody body;

    bool desiredJump;
    bool desiredDash;
    bool sprint;
    bool dashing;
    bool doneDashing;
    float speed;
    float maxSprintSpeed;
    float wallJumpTimer;
    float dashTimer;

    ParticleSystem speedLines;
    CameraFov cameraFov;

    PlayerStats playerStats;

    int jumpPhase, groundContactCount, steepContactCount;

    //minGroundDotProduct describes the maximum angle you can climb on a plain surface
    //minStairsDotProduct describes the maximum angle you can climb on stairs
    float minGroundDotProduct, minStairsDotProduct;

    int stepsSinceLastGrounded, stepsSinceLastJump;

    #region
    //public bool CanJump()
    //{
    //    return playerStats.IsAbilityUnlocked(AbilityType.Jump);
    //}

    //public bool CanDash()
    //{
    //    return playerStats.IsAbilityUnlocked(AbilityType.Dash);
    //}

    //public bool CanSprint()
    //{
    //    return playerStats.IsAbilityUnlocked(AbilityType.Sprint);
    //}

    //public bool CanWallJump()
    //{
    //    return playerStats.IsAbilityUnlocked(AbilityType.WallJump);
    //}

    //public void SetJumpHeight(float addedJumpHeight)
    //{
    //    jumpHeight = jumpHeight + addedJumpHeight;
    //}

    //public void SetAirJump()
    //{
    //    maxAirJumps++;
    //}

    //public void SetMaxDashes()
    //{
    //    dashPhase++;
    //}

    //public void SetDashCooldown()
    //{
    //    dashCooldown = dashCooldown / 2;
    //}

    //public void SetDashForce()
    //{
    //    dashForce += 30f;
    //}

    //public void SetPickUp()
    //{
    //    GetComponentInChildren<PickUpManager>().enabled = true;
    //}

    //public void SetMaxSpeed(float addedSpeed)
    //{
    //    maxSpeed = maxSpeed + addedSpeed;
    //}

    //public void SetCrouch()
    //{
    //    GetComponent<Crouch>().enabled = true;
    //}

    //public PlayerStats GetPlayerStats()
    //{
    //    return playerStats;
    //}

    //void PlayerStatsOnAbilityUnlocked(object sender, PlayerStats.OnAbilityUnlockedEventArgs e)
    //{
    //    switch (e.abilityType)
    //    {
    //        case AbilityType.JumpHeightUp1:
    //        case AbilityType.JumpHeightUp2:
    //            SetJumpHeight(1f);
    //            break;
    //        case AbilityType.AirJumpUp1:
    //        case AbilityType.AirJumpUp2:
    //            SetAirJump();
    //            break;
    //        case AbilityType.AddedDash:
    //            SetMaxDashes();
    //            break;
    //        case AbilityType.DashCooldown:
    //            SetDashCooldown();
    //            break;
    //        case AbilityType.DashForceUp:
    //            SetDashForce();
    //            break;
    //        case AbilityType.PickUp:
    //            SetPickUp();
    //            break;
    //        case AbilityType.MoveSpeedUp1:
    //        case AbilityType.MoveSpeedUp2:
    //            SetMaxSpeed(2f);
    //            break;
    //        case AbilityType.Crouch:
    //            SetCrouch();
    //            break;
    //    }
    //}
    #endregion

    bool OnGround => groundContactCount > 0;
    bool DashGroundReset => OnGround; //Kan vi inte bara använda OnGround direkt?
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

        speed = maxSpeed;
        maxSprintSpeed = maxSpeed * 2f;
        wallJumpTimer = 0f;

        cameraFov = Camera.main.GetComponent<CameraFov>();
        speedLines = GetComponentInChildren<ParticleSystem>();

        //playerStats = new PlayerStats();
        //playerStats.OnAbilityUnlocked += PlayerStatsOnAbilityUnlocked;
    }
    void Update()
    {
        //for (int i = 0; i < collidedWallsList.Count; i++)
        //{
        //    collidedWallsList[i].RemoveFromList();
        //}

        Vector2 playerInput;
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        sprint = Input.GetButton("Sprint") && PlayerStats.sprint && OnGround;
        speed = Mathf.MoveTowards(speed, sprint ? maxSprintSpeed : maxSpeed, maxAcceleration / 4 * Time.unscaledDeltaTime);
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * speed;
        if (wallJumpTimer >= 0)
            wallJumpTimer -= Time.unscaledDeltaTime;

        //if (dashCooldown >= 0)
        //    dashTimer -= Time.unscaledDeltaTime;

        StepAudio();
        AudioJump();
        StepAnimation();

        desiredDash |= Input.GetKeyDown(KeyCode.LeftShift) && PlayerStats.dash && !dashing && dashPhase > 0;

        //With Update and FixedUpdate not always being in sync |= will guarantee that the input is never lost
        desiredJump |= Input.GetButtonDown("Jump") && PlayerStats.jump /*PlayerStats.jump*/ && !desiredDash && !dashing;
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

        if (desiredDash)
        {
            desiredDash = false;
            Dash();
        }

        //if (Time.timeScale < 1)
        //{
        //    velocity.x *= 1 / Time.timeScale;
        //    //body.useGravity = false;
        //    //velocity.y *= 1 / Time.timeScale;
        //    Debug.Log(velocity);
        //    //if (!OnGround)              
        //    //    velocity.y += Physics.gravity.y/ 5;
        //    velocity.z *= 1 / Time.timeScale;

        //    //Debug.Log(velocity);
        //    //Debug.Log(Physics.gravity.y);
        //}

        //if (Time.timeScale == 1)
        //    body.useGravity = true;

        body.velocity = velocity;
        //if (Time.timeScale < 1)
        // body.velocity = Vector3.Scale(body.velocity, new Vector3(1 / Time.timeScale, 1 / Time.timeScale, 1 / Time.timeScale));

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
        {
            //jumpDirection = contactNormal;
            jumpDirection = Vector3.up;

            //Change - correction because removes air jump on wall jump below
            jumpPhase += 1;
        }
        else if (OnSteep && PlayerStats.walljump)
        {

            //if (collidedWallsList.Count > 0 && collidedWallsList[collidedWallsList.Count - 1] == currentWall)
            //    return;

            //jumpDirection = steepNormal;
            jumpDirection = (steepNormal + Vector3.up).normalized;

            wallJumpTimer = wallJumpCooldown;

            //velocity = Vector3.zero;
            //body.AddForce((steepNormal + Vector3.up) * jumpHeight * 40, ForceMode.Impulse);

            //Mathf.Clamp(velocity.y, velocity.y, Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight));

            if (velocity.y < 0)
                velocity.y = 0f;

            //Change - OP
            //jumpPhase = 0;

            //return;
        }
        else if (maxAirJumps > 0 && jumpPhase <= maxAirJumps)
        {
            if (jumpPhase == 0)
                jumpPhase = 1;

            if (velocity.y < 0)
                velocity.y = 0;

            //Change - correction because removes air jump on wall jump below
            jumpPhase += 1;

            //jumpDirection = contactNormal;
            jumpDirection = Vector3.up;
        }
        else
            return;

        stepsSinceLastJump = 0;
        //Change - Removes air jump when wall jumping
        //jumpPhase += 1;
        float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
        //jumpDirection = (jumpDirection + Vector3.up).normalized;
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

    void StepAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            if (pickUp)
                animator.Play("PickUpAnimation");
            else if (throwPickable)
                animator.Play("ThrowAnimation");
            else if (OnGround && desiredVelocity.magnitude > 0)
                animator.Play("WalkingAnimation");
            else if (!OnGround && !dashing && velocity.y > 0)
                animator.Play("JumpAnimation");
            else if (!OnGround && !dashing && velocity.y < 0)
                animator.Play("FallAnimation");
            else
            {
                animator.Play("Idle");
                animator.SetBool("Throw", false);
            }
        }
    }

    void Dash()
    {
        StartCoroutine(Cast());
    }

    IEnumerator Cast()
    {
        dashPhase--;
        //dashTimer = dashCooldown;
        body.useGravity = false;
        velocity = Camera.main.transform.forward * dashForce;
        dashing = true;

        cameraFov.SetCameraFov(dashFov);
        speedLines.Play();

        StartCoroutine(DashCooldown());

        yield return new WaitForSeconds(dashDuration);

        body.velocity = Vector3.zero;
        dashing = false;
        body.useGravity = true;

        cameraFov.SetCameraFov(normalFov);
        speedLines.Stop();
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);

        yield return new WaitUntil(() => DashGroundReset);

        //while (!OnGround)
        //    yield return new WaitForSeconds(0.1f);

        dashPhase++;
    }

    void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);

        //Vector3 normal = collision.GetContact(0).normal;
        //if (normal.y > -0.01f)
        //    currentWall = new CollidedWall(collision.collider, wallJumpCooldown, this);

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

        float accelertaion = OnGround ? maxAcceleration : 1;
        if (!OnGround && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && wallJumpTimer <= 0 && !dashing)
            accelertaion = maxAirAccelertaion;

        if (grappling)
        {
            accelertaion = 5;
        }

        float maxSpeedChange = accelertaion * Time.fixedDeltaTime;

        if (grappling)
        {
            desiredVelocity.x += currentX * 0.95f;
            desiredVelocity.z += currentZ * 0.95f;
        }

        //velocity.y += graplingGun.GetGrappleVelocity().y * Time.fixedDeltaTime;
        //desiredVelocity += graplingGun.GetGrappleVelocity();


        float newX = Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
        float newZ = Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);

        velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
    }

    //public void AddGrappleVelocity(Vector3 grappleVelocity)
    //{
    //    velocity += grappleVelocity;
    //}

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
        if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2 || desiredDash || dashing)
            return false;

        float speed = velocity.magnitude;
        if (speed > maxSnapSpeed)
            return false;

        if (!Physics.Raycast(body.position, Vector3.down, out RaycastHit hit, probeDistance, probeMask))
            return false;

        if (hit.normal.y < GetMinDot(hit.collider.gameObject.layer))
            return false;

        groundContactCount = 1;
        contactNormal = hit.normal;
        float dot = Vector3.Dot(velocity, hit.normal);
        if (dot > 0f)
            velocity = (velocity - hit.normal * dot).normalized * speed;

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
