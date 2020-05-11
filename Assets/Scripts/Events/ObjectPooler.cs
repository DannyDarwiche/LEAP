using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;

    public List<Pool> poolList;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " doesn't excist-");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("Attempted singleton is used more than once");
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        
        foreach (Pool pool in poolList)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject tempObject = Instantiate(pool.prefab);
                tempObject.SetActive(false);
                objectpool.Enqueue(tempObject);
            }
            poolDictionary.Add(pool.tag, objectpool);
        }
        
    }

    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
}
