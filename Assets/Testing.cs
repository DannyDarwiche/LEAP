using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField]
    MovingCharacter player;

    [SerializeField]
    SkillTree skillTree;

    void Start()
    {
        skillTree.SetPlayerStats(player.GetPlayerStats());
    }
}
