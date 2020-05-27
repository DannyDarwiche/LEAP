using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    int spawnAmount = 10;
    [SerializeField]
    float spawnCooldown = 1f;

    void Start()
    {
        GameEvents.currentInstance.OnButtonTriggerOn += Activated;
        //GameEvents.currentInstance.OnButtonTriggerOff += Deactivated;
    }

    public void Activated(int id)
    {
        if (id == this.id)
        {
            Debug.Log("Coroutine start");
            StartCoroutine(SpawnPrefab());
        }
    }

    IEnumerator SpawnPrefab()
    {
        while(spawnAmount > 0)
        {
            Instantiate(prefab, transform);
            spawnAmount--;
            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
