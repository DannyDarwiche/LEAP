using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Hole;
    //public GameObject Door;
    //public GameObject ActivatedDoor;
    [SerializeField]
    public int ID;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == Hole)
        {
            GameEvents.currentInstance.ButtonTriggerOn(ID);
            //ActivatedDoor.SetActive(true);
            //Debug.Log("Collision");
        }
    }   
}
