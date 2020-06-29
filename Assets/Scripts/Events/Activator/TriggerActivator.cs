using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Player, Pickable
}

public class TriggerActivator : MonoBehaviour
{
    //Isak and Danny
    //Activates event when entering a trigger collider.

    [SerializeField]
    float id;

    [SerializeField]
    ItemType[] itemType;

    List<GameObject> savedItems;

    void Start()
    {
        savedItems = new List<GameObject>();
    }

    void OnTriggerEnter(Collider other)
    {
        foreach (ItemType type in itemType)
        {
            if (other.CompareTag(type.ToString()))
            {
                savedItems.Add(other.gameObject);
                GameEvents.currentInstance.TriggerOn(id);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (savedItems.Contains(other.gameObject))
        {
            savedItems.Remove(other.gameObject);
            if (savedItems.Count == 0)
                GameEvents.currentInstance.TriggerOff(id);
        }
    }
}
