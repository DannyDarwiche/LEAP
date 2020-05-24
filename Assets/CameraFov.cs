using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFov : MonoBehaviour
{
    Camera playerCamera;
    float targetFov;
    float fov;

    void Awake()
    {
        playerCamera = GetComponent<Camera>();
        targetFov = playerCamera.fieldOfView;
        fov = targetFov;
    }

    void Update()
    {
        float fovSpeed = 6f;
        fov = Mathf.Lerp(fov, targetFov, Time.deltaTime * fovSpeed);
        playerCamera.fieldOfView = fov;
    }

    public void SetCameraFov(float targetFov)
    {
        this.targetFov = targetFov;
    }
}
