using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public ItemObject weapon;
    public float damageBonus;
    public float deffenceBonus;

    public void Awake()
    {
        type = ItemType.Weapon;
    }
}