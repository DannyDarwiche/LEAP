using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public GameObject White1;
    public GameObject White2;
    public GameObject White3;
    public GameObject Blue1;
    [SerializeField]
    public int ID;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == White1)
        {
            Destroy(White1);
            Destroy(White2);
            Destroy(White3);
            Blue1.SetActive(true);
            //GameEvents.currentInstance.ButtonTriggerOn(ID);
            //ActivatedDoor.SetActive(true);
            //Debug.Log("Collision");

        }
    }
}
