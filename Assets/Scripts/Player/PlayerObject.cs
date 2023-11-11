using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Object", menuName = "Inventory System/Player/Player")]
public class PlayerObject :ScriptableObject
{
    public float healthPoint;
    public float maxHealth;
    public float attack;
    public float moveSpeed;
    public float defense;
    public float attackSpeed;
    public void Reset()
    {
        healthPoint = maxHealth;
    }

    public void upgradeStatistic(string element, float amount)
    {
        switch (element) {
            case "maxHealth":
                maxHealth += amount; 
                break;
        }
    }


}