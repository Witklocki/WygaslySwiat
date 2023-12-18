using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponModel;
using QuestItemModel;
public class DB : MonoBehaviour
{
    public NPCList NPCList;
    public bool npcSavingInProgres = false;
    public DropObjectController playerDrops;
    public WeaponObjectDataController weaponObject;
    public QuestItems questItems;
    public WeaponData weapons
    {
        get => weaponObject.weaponObject;
    }
    public QuestItemList quests
    {
        get => questItems.qObj;
    }
    private void Awake()
    {
        NPCList = new NPCList();
        playerDrops = new DropObjectController();
        weaponObject = new WeaponObjectDataController();
        questItems = new QuestItems();
    }
}
