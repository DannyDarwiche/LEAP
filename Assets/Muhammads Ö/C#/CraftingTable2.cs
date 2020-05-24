using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable2 : MonoBehaviour
{
    public GameObject White1, White2, White3, Blue1;
    public bool White1isTrue, White2isTrue, White3isTrue;
    [SerializeField]
    public int ID;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == White1)
        {
            White1isTrue = true;
        }
        else if (col.gameObject == White2)
        {
            White2isTrue = true;
        }
        //else if (col.gameObject == White3)
        //{
        //    White3isTrue = true;
        //}

        else
        {
            White1isTrue = false;
            White2isTrue = false;
            White3isTrue = false;
        }

        if(White1isTrue && White2isTrue)
        {

        Destroy(White1);
        Destroy(White2);
        //Destroy(White3);
        Blue1.SetActive(true);

        }


    }
}
