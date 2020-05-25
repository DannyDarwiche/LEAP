using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField]
    AbilityType abilityType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SkillTree.instance.gameObject.SetActive(true);
            GameEvents.currentInstance.UpgradeGet(abilityType);
            SkillTree.instance.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
