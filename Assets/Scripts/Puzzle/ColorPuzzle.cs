using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorPuzzle : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    ColorMatch[] colorMatches;

    bool completed;
    bool firstcall = true;
    void Update()
    {
        completed = CheckIfCompleted();
        if (completed && firstcall)
        {
            GameEvents.currentInstance.PuzzleSolvedTrigger(id);
            firstcall = false;
        }
        else if (firstcall)
        {
            GameEvents.currentInstance.PuzzleFailedTrigger(id);
            firstcall = false;
        }
    }

    bool CheckIfCompleted()
    {
        foreach (ColorMatch platform in colorMatches)
            if (!platform.correctCube)
            {
                if (completed)
                    firstcall = true;
                return false;
            }
        if (!completed)
            firstcall = true;
        return true;
    }
}
