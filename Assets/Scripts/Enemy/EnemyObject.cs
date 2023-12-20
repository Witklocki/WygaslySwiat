using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {
    Normal,
    Tank,
    Boomer,

}

public class EnemyObject : ScriptableObject
{
    public float healthPoint;
    public float maxHealth;
    public float healthMultiplayer;
    public float attack;
    public float attackMultiplayer;
    public float moveSpeed;
    public float runSpeed;
    public float viewRadius;
    public float viewAngle; 


    public EnemyType type;


    public EnemyObject()
    {
        healthPoint = 100;
        maxHealth = 100;
        healthMultiplayer = 1;
        attack = 10;
        attackMultiplayer = 1;
        moveSpeed =1.75f;
        runSpeed =2.8f;
        viewRadius = 10;
        viewAngle = 360;
        type = EnemyType.Normal;
    }

}

