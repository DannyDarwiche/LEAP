using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("WALKING")]
    public float speed;

    Rigidbody rb;
    Vector3 moveDirection;

    [Header("JUMPING")]
    public float jumpForce = 7;

    public Transform groundCheck;
    public float groundCheckRadius = 0.4f;
    public LayerMask groundMask;
    public LayerMask interactableMask;
    //public LayerMask nonPlayerMoveableMask;

    bool isGrounded;

    public bool rotating = false;

    public float hoverTimer = 2;

    public float jetpackFuel = 100;

    bool jumped = false;
    bool canMidAirJump = false;
    bool resetAbilities = false;
    float inventorySlot = 1;
    float resetAbilitiesTimer = 0.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            inventorySlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            inventorySlot = 2;

        if (rotating)
            return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask | interactableMask /*| nonPlayerMoveableMask*/);
        if (isGrounded && resetAbilities)
        {
            canMidAirJump = false;
            jumped = false;
            hoverTimer = 2;
            jetpackFuel = 100;
        }

        if (!resetAbilities)
        {
            resetAbilitiesTimer -= Time.deltaTime;
            if(resetAbilitiesTimer <= 0)
            {
                resetAbilities = true;
                resetAbilitiesTimer = 0.5f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isGrounded = false;
            jumped = true;
            Debug.Log("Jumped");
            resetAbilities = false;
        }
        if (Input.GetKeyUp(KeyCode.Space) && jumped && !isGrounded)
        {
            canMidAirJump = true;
        }

        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
    }

    private void FixedUpdate()
    {
        //if (rotating)
        //{
        //    moveDirection = Vector3.zero;
        //}

        Vector3 yVelFix = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = moveDirection * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && !isGrounded && canMidAirJump)
        {
            if (inventorySlot == 1 && hoverTimer > 0)
            {
                hoverTimer -= Time.deltaTime;
                //Vector3 saveVelocity = new Vector3(rb.velocity.x, 0, )
                // rb.velocity.y
                yVelFix = Vector3.zero;
            }
            if (inventorySlot == 2 && jetpackFuel > 0)
            {
                jetpackFuel -= Time.deltaTime * 20; //20 ska motsvara fuelconsumption
                //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                yVelFix = new Vector3(0, 5, 0);
            }
        }
        rb.velocity += yVelFix;
        //Move();
    }

    //void Move()
    //{
    //    Vector3 yVelFix = new Vector3(0, rb.velocity.y, 0);
    //    rb.velocity = moveDirection * speed * Time.deltaTime;
    //    rb.velocity += yVelFix;
    //}

    //void Jump()
    //{
    //    rb.AddForce(0, jumpForce * Time.deltaTime, 0);
    //}

    //void Hover()
    //{
    //    if (Input.GetKey(KeyCode.Space) && !isGrounded && hoverTimer > 0)
    //    {
    //        hoverTimer -= Time.deltaTime;
    //        Vector3 saveVelocity = new Vector3(rb.velocity.x, 0, )
    //        rb.velocity.y 
    //    }
    //}

    void Jetpack()
    {

    }
}
