using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    [SerializeField]
    bool canHold = true;
    [SerializeField]
    GameObject item;
    [SerializeField]
    GameObject tempParent;
    [SerializeField]
    bool isHolding = false;

    float throwForce = 600;
    Vector3 objectPos;
    float distance;

    void Update()
    {
        //if (isHolding)
        //{
        //    item.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //    item.transform.SetParent(tempParent.transform);

        //    //if (Input.GetMouseButton(1))
        //    //Throw();
        //}
        //else
        //{
        //    objectPos = item.transform.position;
        //    item.transform.SetParent(null);
        //    item.GetComponent<Rigidbody>().useGravity = true;
        //    item.transform.position = objectPos;
        //}
    }

    void OnMouseDown()
    {
        isHolding = true;
        item.transform.SetParent(tempParent.transform);
        item.transform.localPosition = Vector3.zero;
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().detectCollisions = true;
    }

    void OnMouseDrag()
    {
        item.GetComponent<Rigidbody>().velocity = Vector3.zero;
        item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4.312f);
        //Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
        //transform.position = cursorPosition;
    }

    void OnMouseUp()
    {
        isHolding = false;
        item.transform.SetParent(null);
        item.GetComponent<Rigidbody>().useGravity = true;
    }

    //[SerializeField]
    //float forceFactor = 10f;

    //private Vector3 screenPoint;
    //private Vector3 offset;
    //Rigidbody rigidbody;

    //void Start()
    //{
    //    rigidbody = GetComponent<Rigidbody>();   
    //}

    //void OnMouseDown()
    //{
    //    rigidbody.useGravity = false;
    //    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    //    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    //}

    //void OnMouseUp()
    //{
    //    rigidbody.useGravity = true;
    //}

    //void OnMouseDrag()
    //{
    //    Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4.312f);
    //    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
    //    //transform.position = cursorPosition;

    //    rigidbody.MovePosition(cursorPosition);

    //Debug.Log(rigidbody.velocity);
    //Debug.Log("Offset Magnitude: " + offset.magnitude);
    //Debug.Log("Distance Magnitude: " + (cursorPosition - (transform.position + offset)).magnitude);

    //if ((cursorPosition - (transform.position + offset)).magnitude < offset.magnitude)
    //{
    //    rigidbody.velocity = Vector3.zero;
    //}

    //rigidbody.AddForceAtPosition((cursorPosition - (transform.position + offset)) * forceFactor, transform.position + offset);
    //}
}
