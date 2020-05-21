using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStay : MonoBehaviour
{
    public GameObject Plate;
    public GameObject Door;
    public GameObject ActivatedDoor;
    [SerializeField]
    public int ID;
    bool isWaiting = false;

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject == Plate)
        {
            GameEvents.currentInstance.ButtonTriggerOn(ID);
            //ActivatedDoor.SetActive(true);
            //Debug.Log("Collision");
        }
    }
}
