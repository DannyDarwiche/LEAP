using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    Vector3 moveDirection;
    [SerializeField]
    float moveLength, moveSpeed;

    bool activated;
    int activeState;
    Vector3 startPosition;

    public void Activated(int id)
    {
        if (id == this.id)
        {
            activeState = 1;
            activated = true;
        }
    }

    public void Deactivated(int id)
    {
        if (id == this.id)
        {
            activeState = 0;
            activated = false;
        }
    }

    void Start()
    {
        GameEvents.currentInstance.OnButtonTriggerOn += Activated;
        GameEvents.currentInstance.OnButtonTriggerOff += Deactivated;
        GameEvents.currentInstance.OnPlatformTriggerOn += Activated;
        GameEvents.currentInstance.OnPlatformTriggerOff += Deactivated;
        startPosition = transform.position;
        moveDirection.Normalize();
    }

    void Update()
    {
        Vector3 maxMove = moveDirection * activeState * moveLength;
        float maxSpeedChange = moveSpeed * Time.deltaTime;
        float newX = Mathf.MoveTowards(transform.position.x, startPosition.x + maxMove.x, maxSpeedChange);
        float newY = Mathf.MoveTowards(transform.position.y, startPosition.y + maxMove.y, maxSpeedChange);
        float newZ = Mathf.MoveTowards(transform.position.z, startPosition.z + maxMove.z, maxSpeedChange);
        Vector3 moveDistance = new Vector3(newX, newY, newZ);
        transform.SetPositionAndRotation(moveDistance, transform.rotation);
    }

    void OnDestroy()
    {
        GameEvents.currentInstance.OnButtonTriggerOn -= Activated;
        GameEvents.currentInstance.OnButtonTriggerOff -= Deactivated;
        GameEvents.currentInstance.OnPlatformTriggerOn -= Activated;
        GameEvents.currentInstance.OnPlatformTriggerOff -= Deactivated;
    }
}
