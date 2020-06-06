using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEventPP : MonoBehaviour
{
    //Isak, Danny, Ai, Alexander and Muhammad
    //Moves object activated by pressure plate.

    [SerializeField]
    int id;
    [SerializeField]
    Vector3 moveDirection;
    [SerializeField]
    float moveLength, moveSpeed;

    bool activated;
    float activeState;
    Vector3 startPosition;

    void Start()
    {
        GameEvents.currentInstance.OnPreasureplateTriggerOn += Activated;
        GameEvents.currentInstance.OnPreasureplateTriggerOff += Deactivated;
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

    void Activated(int id, float percentage)
    {
        if (id == this.id)
        {
            activeState = percentage;
            if (percentage > 0)
                activated = true;
        }
    }

    void Deactivated(int id, float percentage)
    {
        if (id == this.id)
        {
            activeState = percentage;
            if (percentage == 0)
            {
                activeState = 0;
                activated = false;
            }
        }
    }

    void OnDestroy()
    {
        GameEvents.currentInstance.OnPreasureplateTriggerOn -= Activated;
        GameEvents.currentInstance.OnPreasureplateTriggerOff -= Deactivated;
    }
}
