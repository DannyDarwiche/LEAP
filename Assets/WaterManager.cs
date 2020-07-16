using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    MeshFilter meshFilter;
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }
    void Update()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
            vertices[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + vertices[i].x, true);

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();       
    }
}
