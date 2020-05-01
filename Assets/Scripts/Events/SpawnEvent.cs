using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEvent : MonoBehaviour
{
    public bool spawning;

    [SerializeField]
    int id; 
    [SerializeField]
    string spawnedObjectTag;
    [SerializeField, Range(0,1)]
    float spawnTime;
    [SerializeField, Range(0, 100)]
    float spawnedObjectLifeTime;
    [SerializeField]
    float minXRotation, maxXRotation, rotationStep;
    ObjectPooler objectpooler;
    float timer;
    public void Activated(int id)
    {
        if(id == this.id)
            spawning = true;
    }
    public void Deactivated(int id)
    {
        if(id == this.id)
            spawning = false;
    }
    void Start()
    {
        GameEvents.currentInstance.OnButtonTriggerOn += Activated;
        GameEvents.currentInstance.OnButtonTriggerOff += Deactivated;

        objectpooler = ObjectPooler.instance;
    }
    void FixedUpdate()
    {
        if (spawning)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                objectpooler.SpawnFromPool(spawnedObjectTag, this.transform.position, this.transform.rotation);
                timer = 0;
            }
        }
    }
}
