using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzle : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    ColorMatch[] colorMatches;
    bool completed;
    void Update()
    {
        completed = CheckIfCompleted();
        if (completed)
        {
            GameEvents.currentInstance.PuzzleSolvedTrigger(id);
            completed = false;
        }
    }

    bool CheckIfCompleted()
    {
        foreach (ColorMatch platform in colorMatches)
            if (!platform.correctCube)
                return false;
        return true;
    }
}
