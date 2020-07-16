using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public float amplitude = 1f, length = 2f, speed = 1f, offset = 0f;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Instance already exsists");
            Destroy(this);
        }
    }

    void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight(float x, bool input = false)
    {
        if (input)
            return amplitude * Mathf.Sin(x / length + offset);
        else
            return transform.position.y + 12 + amplitude * Mathf.Sin(x / length + offset);
    }
}
