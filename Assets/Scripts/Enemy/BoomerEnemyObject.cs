using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boomer Object", menuName = "Inventory System/Enemy/Boomer")]

public class BoomerEnemyObject : EnemyObject
{

    private void Awake()
    {
        type = EnemyType.Boomer;
        healthPoint = 20.0f;
        maxHealth = 20.0f;
        healthMultiplayer = 1.0f;
        attack = 5.0f;
        attackMultiplayer = 1.0f;
        moveSpeed = 2.5f;
        runSpeed = 3.0f;
        viewRadius = 12.5f;
        viewAngle = 360.0f;
    }
}
