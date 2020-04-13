using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //// X means rotation around the x axis (up and down)
    //public float minimumX = -60f;
    //public float maximumX = 60f;
    //// Y means rotation around the y axis (left and right)
    //public float minimumY = -360f;
    //public float maximumY = 360f;

    //public float sensitivityX = 15f;
    //public float sensitivityY = 15f;

    //public Camera cam;

    //float rotationY = 0f;
    //float rotationX = 0f;

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //rotationY += Input.GetAxis("Mouse X") * sensitivityY * Time.deltaTime;
        //rotationX += Input.GetAxis("Mouse Y") * sensitivityX * Time.deltaTime;

        //rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

        //transform.localEulerAngles = new Vector3(0, rotationY, 0);
        //cam.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
