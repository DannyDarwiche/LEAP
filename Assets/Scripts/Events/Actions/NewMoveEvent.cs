using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMoveEvent : Event
{
    //Isak, Danny, Ai, Alexander and Muhammad
    //Moves object activated by button.

    [SerializeField]
    bool loop;
    [SerializeField]
    Vector3 moveDirection;
    [SerializeField]
    float moveLength, moveSpeed;

    Vector3 startPosition;

    void Start()
    {
        GameEvents.currentInstance.OnButtonTriggerOn += Activated;
        GameEvents.currentInstance.OnButtonTriggerOff += Deactivated;
        GameEvents.currentInstance.OnPreasureplateTriggerOn += Activated;
        GameEvents.currentInstance.OnPreasureplateTriggerOff += Deactivated;
        GameEvents.currentInstance.OnTriggerOn += Activated;
        GameEvents.currentInstance.OnTriggerOff += Deactivated;

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
        if (loop && transform.position == startPosition + maxMove && activated)
            activeState = loopActiveState - activeState;
    }

    void OnDestroy()
    {
        GameEvents.currentInstance.OnButtonTriggerOn -= Activated;
        GameEvents.currentInstance.OnButtonTriggerOff -= Deactivated;
        GameEvents.currentInstance.OnPreasureplateTriggerOn -= Activated;
        GameEvents.currentInstance.OnPreasureplateTriggerOff -= Deactivated;
        GameEvents.currentInstance.OnTriggerOn -= Activated;
        GameEvents.currentInstance.OnTriggerOff -= Deactivated;
    }
}
