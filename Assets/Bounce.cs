using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField]
    float bounceForce;

    [SerializeField]
    float maxImpactForce;

    int counter;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.attachedRigidbody == null)
            return;
        Debug.Log(collision.impulse.magnitude);
        //if (collision.impulse.magnitude > maxImpactForce)
        //    return;

        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.Space))
        {
            counter++;
            Debug.Log("Bounced!" + counter);
            //collision.collider.attachedRigidbody.velocity =
            //    new Vector3(collision.collider.attachedRigidbody.velocity.x,
            //     0,
            //      collision.collider.attachedRigidbody.velocity.z);

            float percentage = (collision.impulse.magnitude / maxImpactForce);
            percentage = Mathf.Clamp01(percentage);
            Debug.Log("Percentage: " + percentage);
            collision.collider.attachedRigidbody.AddForce(transform.up * bounceForce * percentage, ForceMode.Impulse);
        }
        else
        {
            collision.collider.attachedRigidbody.velocity *= 0.9f;
            if (collision.impulse.magnitude < 350)
                collision.collider.attachedRigidbody.velocity *= 0.5f;
        }
    }
}
