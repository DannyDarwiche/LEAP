using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : MonoBehaviour
{
    [SerializeField]
    Vector3 moveDirection;
    [SerializeField]
    float moveLength, moveSpeed;

    bool activated;
    float activatedPercentage;
    Vector3 startPosition;

    public void Activated(float percent = 1)
    {
        activatedPercentage = percent;
        if (percent > 0)
            activated = true;
    }
    public void Deactivated()
    {
        activatedPercentage = 0;
        activated = false;
    }
    void Start()
    {
        startPosition = transform.position;
        moveDirection.Normalize();
    }
    void Update()
    {
        Vector3 maxMove = moveDirection * activatedPercentage * moveLength;
        float maxSpeedChange = moveSpeed * Time.deltaTime;
        float newX = Mathf.MoveTowards(transform.position.x, startPosition.x + maxMove.x, maxSpeedChange);
        float newY = Mathf.MoveTowards(transform.position.y, startPosition.y + maxMove.y, maxSpeedChange);
        float newZ = Mathf.MoveTowards(transform.position.z, startPosition.z + maxMove.z, maxSpeedChange);
        Vector3 moveDistance = new Vector3(newX, newY, newZ);
        transform.SetPositionAndRotation(moveDistance, transform.rotation);
    }
}
