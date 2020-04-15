using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDestManager : MonoBehaviour
{
    public Camera playerCamera;
    Vector3 oldCameraPos = new Vector3(0,0,0);
    float yAxis, minY, maxY = 2;
    public GameObject item;

    void Update()
    {
        //RaycastHit hit;
        //Physics.Raycast(item.transform.position, Vector3.down, out hit);
        //minY = item.transform.InverseTransformPoint(hit.point).y + item.transform.localScale.y;
        minY = item.transform.localScale.y / -2;
        yAxis = Mathf.Tan(oldCameraPos.x - playerCamera.transform.rotation.x) * transform.localPosition.z;
        float yClamp = Mathf.Clamp(yAxis, minY, maxY);
        transform.localPosition = new Vector3(transform.localPosition.x, yClamp, transform.localPosition.z);
        //transform.localPosition = new Vector3(transform.localPosition.x, yAxis, transform.localPosition.z);
    }
}
