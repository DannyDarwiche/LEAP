using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField]
    Transform startPoint;

    [SerializeField]
    Transform endPoint;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPoint.transform.position);
    }

    void LateUpdate()
    {
        lineRenderer.SetPosition(1, endPoint.transform.position);
    }
}
