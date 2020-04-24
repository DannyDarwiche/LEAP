using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public Camera cam;
    public float interactDistance;

    public Transform holdPosition;
    public float attractSpeed;

    public float minThrowForce;
    public float maxThrowForce;
    float throwForce;

    GameObject objectIHave;
    Rigidbody objectRigidbody;

    Vector3 rotateVector = Vector3.one;

    bool hasObject = false;

    void Start()
    {
        throwForce = minThrowForce;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasObject)
            DoRay();

        if (Input.GetMouseButton(1) && hasObject)
            throwForce += 0.1f;

        if (Input.GetMouseButtonUp(1) && hasObject)
            ShootObject();

        if (Input.GetKeyDown(KeyCode.G) && hasObject)
            DropObject();

        if (hasObject)
        {
            DropObject();
            if (CheckDistance() >= 1f)
                MoveObjectToPosition();
        }
    }

    void CalculateRotationVector()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        float z = Random.Range(-0.5f, 0.5f);

        rotateVector = new Vector3(x, y, z);
    }

    void RotateObject()
    {
        objectIHave.transform.Rotate(rotateVector);
    }

    public float CheckDistance()
    {
        float distance = Vector3.Distance(objectIHave.transform.position, holdPosition.transform.position);
        return distance;
    }

    void MoveObjectToPosition()
    {
        objectIHave.transform.position = Vector3.Lerp(objectIHave.transform.position, holdPosition.position, attractSpeed * Time.deltaTime);
    }

    void DropObject()
    {
        objectRigidbody.constraints = RigidbodyConstraints.None;
        objectIHave.transform.parent = null;
        objectIHave = null;
        hasObject = false;
    }

    void ShootObject()
    {
        throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
        objectRigidbody.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        throwForce = minThrowForce;
        DropObject();
    }

    void DoRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactDistance))
        {
            objectIHave = hit.collider.gameObject;
            objectIHave.transform.SetParent(holdPosition);

            objectRigidbody = objectIHave.GetComponent<Rigidbody>();
            objectRigidbody.constraints = RigidbodyConstraints.FreezeAll;

            hasObject = true;

            CalculateRotationVector();
        }
    }
    //Källa: https://www.youtube.com/watch?v=v2ofnNLXdoI&list=PLfRfKfCcFo_sVdoFRqxUpjNyamd5P9NOm&index=6
}
