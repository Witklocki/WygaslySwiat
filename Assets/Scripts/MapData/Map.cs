using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public MapData map;
    NPCList NPCList;
    WeaponObjectDataController weaponObjectDataController;
    [SerializeField] List<GameObject> npcPrefab;
    [SerializeField] List <GameObject> weaponPrefab;

    private void Start()
    {
        NPCList = new NPCList();
        weaponObjectDataController = new WeaponObjectDataController();

        if(map.name == "SavePlace")
        {
            GenerateSavedNPC();
            GenerateWeapon();
        }
        
    }

    private void Update()
    {

    }


   public void GenerateSavedNPC()
    {
        for (int i = 0; i < NPCList.data.npc.Length; i++)
        {
            if (NPCList.data.npc[i].isSaved)
            {
                npcPrefab[i].gameObject.SetActive(true);
            }
        }
    }

    public void GenerateWeapon()
    {
        for (int i = 0; i < weaponObjectDataController.weaponObject.weapons.Length ;i++)
        {
            if (weaponObjectDataController.weaponObject.weapons[i].isUnlocker)
            {
                weaponPrefab[i].gameObject.SetActive(true);
            }
        }
    }
}
