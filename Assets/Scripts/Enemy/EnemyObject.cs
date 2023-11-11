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
}
