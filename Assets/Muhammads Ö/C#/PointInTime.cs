using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public Collision col;

    public PointInTime (Vector3 _position, Quaternion _rotation, Collision _col)
    {
        position = _position;
        rotation = _rotation;
        _col = col;
    }

}
