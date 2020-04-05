using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBoard : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject player;

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxAccerleration = 10f;

    int layerMask;
    public float hoverForce = 9.0f;
    public float hoverHeight = 2.0f;
    public GameObject[] hoverPoints;

    public Transform playerParent;
    bool inUse = false;

    float speed = 400;
    Vector3 moveDirection;
    Vector3 velocity, desiredVelocity;

    private void Start()
    {
        // rb = GetComponent<Rigidbody>();
        layerMask = LayerMask.GetMask("groundMask");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.GetComponent<Rigidbody>().isKinematic = false;
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<Transform>().parent = null;
            player.GetComponent<Transform>().rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, 0);
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * 7, ForceMode.Impulse);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            //rb.velocity = Vector3.zero;
            inUse = false;

        }
        //if (transform.rotation.eulerAngles.x < -90 || transform.rotation.eulerAngles.x > 90)
        //{
        //    transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //    rb.velocity = Vector3.zero;
        //    Debug.Log("X");
        //}
        //if (transform.rotation.eulerAngles.z < -90 || transform.rotation.eulerAngles.z > 90)
        //{
        //    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        //    rb.velocity = Vector3.zero;

        //    Debug.Log("Z");
        //}

        if (inUse)
        {
            Vector2 playerInput;
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");
            playerInput = Vector2.ClampMagnitude(playerInput, 1f);

            desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

            //float horizontalMovement = Input.GetAxisRaw("Horizontal");
            //float verticalMovement = Input.GetAxisRaw("Vertical");
            //moveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;


            //velocity = rb.velocity;
            float maxSpeedChange = maxAccerleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            //Vector3 displacement = velocity * Time.deltaTime;
            //transform.localPosition += displacement;

            //rb.velocity = velocity;
        }
    }

    private void FixedUpdate()
    {
        if (inUse)
        {
            //rb.velocity = moveDirection * speed * Time.deltaTime;

            velocity = rb.velocity;
            float maxSpeedChange = maxAccerleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            rb.velocity = velocity;

        }

        RaycastHit hit;

        for (int i = 0; i < hoverPoints.Length; i++)
        {
            var hoverPoint = hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, hoverHeight, layerMask))
            {
                rb.AddForceAtPosition(Vector3.up * hoverForce * (1f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
            }
            else
            {
                if (transform.position.y > hoverPoint.transform.position.y)
                {
                    rb.AddForceAtPosition(hoverPoint.transform.up * hoverForce, hoverPoint.transform.position);
                    Debug.Log("Working");
                }
                else
                {
                    rb.AddForceAtPosition(hoverPoint.transform.up * -hoverForce, hoverPoint.transform.position);
                    Debug.Log("Not Working");
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.Find("Player"))
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            other.transform.parent = playerParent;
            other.transform.position = playerParent.transform.position;
            other.transform.rotation = playerParent.transform.rotation;
            // other.GetComponentInChildren<CameraController>().playerBody = this.transform;
            other.GetComponent<PlayerController>().enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            inUse = true;
        }
    }
}
