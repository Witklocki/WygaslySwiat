using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponModel;

public class DB : MonoBehaviour
{
    public NPCList NPCList;
    public bool npcSavingInProgres = false;
    public DropObjectController playerDrops;
    public WeaponObjectDataController weaponObject;

    public WeaponData weapons
    {
        get => weaponObject.weaponObject;
    }

    private void Awake()
    {
        NPCList = new NPCList();
        playerDrops = new DropObjectController();
        weaponObject = new WeaponObjectDataController();
        for (int i = 0; i < weaponObject.weaponObject.weapons.Length; i++)
        {
            print(weaponObject.weaponObject.weapons[i].name);
        }
    }
}
