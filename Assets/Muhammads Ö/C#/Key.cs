using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Hole;
    //public GameObject Door;
    //public GameObject ActivatedDoor;
    [SerializeField]
    public float id;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == Hole)
        {
            GameEvents.currentInstance.ButtonTriggerOn(id);
            //ActivatedDoor.SetActive(true);
            //Debug.Log("Collision");
        }
    }   
}
