//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RotateEvent : MonoBehaviour
//{
//    //Isak
//    //Rotates object when activated by button.

//    [SerializeField]
//    float id;
//    [SerializeField]
//    Vector3 rotationVector;
//    [SerializeField]
//    float rotationSpeed;

//    bool activated;

//    void Start()
//    {
//        GameEvents.currentInstance.OnButtonTriggerOn += Activated;
//        GameEvents.currentInstance.OnButtonTriggerOff += Deactivated;
//    }

//    void Update()
//    {
//        if(activated)
//            transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
//    }

//    void Activated(float id)
//    {
//        if (id == this.id)
//            activated = true;
//    }

//    void Deactivated(float id)
//    {
//        if (id == this.id)
//            activated = false;
//    }
//}
