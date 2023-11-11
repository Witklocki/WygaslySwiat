using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Object", menuName = "Inventory System/Enemy/Normal")]

public class NormalEnemyObject : EnemyObject
{

    private void Awake()
    {
        type = EnemyType.Normal;
    }

}
