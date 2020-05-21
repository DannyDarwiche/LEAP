using System.Collections.Generic;
using UnityEngine;

public class GraplingGun : MonoBehaviour
{
    [SerializeField]
    MovingCharacter player;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    Transform gun;
    [SerializeField]
    int whatToGrapple;
    [SerializeField]
    float maxDistance, minDistance;
    [SerializeField]
    float rotationSmooth;

    [SerializeField]
    float raycastRadius;
    [SerializeField]
    int raycastCount;

    [SerializeField]
    float pullForce;
    [SerializeField]
    float pushForce;
    [SerializeField]
    float yMultiplier;
    [SerializeField]
    float minPhysicsDistance;
    [SerializeField]
    float maxPhysicsDistance;


    [SerializeField]
    LineRenderer lineRenderer;

    Vector3 currentGrapplePosition;

    Vector3 hit;

    Vector3 velocity;

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && Raycast(out RaycastHit hit))
        {
            this.hit = hit.point;

            lineRenderer.positionCount = 2;
            currentGrapplePosition = firePoint.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.positionCount = 0;
        }

        //if (Input.GetMouseButtonDown(0))
        //{

        //}
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.LookRotation(-(gun.position - hit)), rotationSmooth * Time.fixedDeltaTime);

            float distance = Vector3.Distance(player.transform.position, hit);
            if (!(distance >= minPhysicsDistance) || !(distance <= maxPhysicsDistance))
                return;

            velocity += pullForce * Time.fixedDeltaTime * yMultiplier * Mathf.Abs(hit.y - player.transform.position.y) * (hit - player.transform.position).normalized;
            velocity += pushForce * Time.fixedDeltaTime * player.transform.forward;
        }
        else
        {
            gun.localRotation = Quaternion.Lerp(gun.localRotation, Quaternion.Euler(0, 0, 0), rotationSmooth * Time.fixedDeltaTime);
            velocity = Vector3.zero;
        }
    }

    void DrawRope()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, hit, Time.deltaTime * 8f);

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, currentGrapplePosition);
    }

    public Vector3 GetGrappleVelocity()
    {
        return velocity;
    }

    bool Raycast(out RaycastHit hit)
    {
        float diveded = raycastRadius / 2f;
        List<RaycastHit> possible = new List<RaycastHit>();
        Transform cam = Camera.main.transform;

        for (int x = 0; x < raycastCount; x++)
        {
            for (int y = 0; y < raycastCount; y++)
            {
                Vector2 pos = new Vector2(
                    Mathf.Lerp(-diveded, diveded, x / (float)(raycastCount - 1)),
                    Mathf.Lerp(-diveded, diveded, y / (float)(raycastCount - 1)));

                if (!Physics.Raycast(cam.position + cam.right * pos.x + cam.up * pos.y, cam.forward, out hit, maxDistance))
                    continue;

                float distance = Vector3.Distance(cam.position, hit.point);
                //if (hit.transform.gameObject.layer != whatToGrapple)
                //    continue;
                if (distance < minDistance)
                    continue;
                if (distance > maxDistance)
                    continue;
                possible.Add(hit);
            }
        }

        RaycastHit[] arr = possible.ToArray();
        possible.Clear();

        if (arr.Length > 0)
        {
            RaycastHit closest = new RaycastHit();
            float distance = 0f;
            bool set = false;

            foreach (RaycastHit h in arr)
            {
                float hitDistance = DistanceFromCenter(h.point);

                if (!set)
                {
                    set = true;
                    distance = hitDistance;
                    closest = h;
                }
                else if (hitDistance < distance)
                {
                    distance = hitDistance; ;
                    closest = h;
                }
            }
            hit = closest;
            return true;

        }

        hit = new RaycastHit();
        return false;
    }

    float DistanceFromCenter(Vector3 point)
    {
        return Vector2.Distance(Camera.main.WorldToViewportPoint(point), new Vector2(0.5f, 0.5f));
    }
}