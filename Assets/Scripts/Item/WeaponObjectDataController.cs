using DropModel;
using System;
using System.IO;
using UnityEngine;
using WeaponModel;

namespace WeaponModel
{


    [Serializable]
    public class WeaponObjectData
    {
        public int id;
        public string name;
        public bool isUnlocker;
        public DropObject price;

        public int commonElement
        {
            get => price.commonElement;
        }
        public int basicElement
        {
            get => price.basicElement;
        }
        public int rareElemnt
        {
            get => price.rareElemnt;
        }
        public int elitElemnts
        {
            get => price.elitElemnts;
        }
    }


    [Serializable]
    public class WeaponData
    {
        public WeaponObjectData[] weapons;

        public WeaponObjectData GetWeaponObjectWithId(int id)
        {
            return weapons[id];
        }
    }
}

[CreateAssetMenu(fileName = "Weapon Data Contorller", menuName = "Inventory System/WeaponsData")]
public class WeaponObjectDataController : ScriptableObject
{

    public WeaponData weaponObject;

    private static readonly string fileName = "Assets/Resources/Data/WaeponData.json";

    public WeaponObjectDataController() 
    {
        readJson();
    }

    public void readJson()
    {
        string jsonString = File.ReadAllText(fileName);
        weaponObject = JsonUtility.FromJson<WeaponData>(jsonString);
    }

    public void readJson(string path)
    {
        string jsonString = File.ReadAllText(path);
        weaponObject = JsonUtility.FromJson<WeaponData>(jsonString);
    }

    public void writeJson()
    {
        string jsonString = JsonUtility.ToJson(weaponObject, true);
        File.WriteAllText(fileName, jsonString);
    }

    public void unlockWeapon(int id)
    {
       Debug.Log(id);
       for (int i = 0; i < weaponObject.weapons.Length; i++)
        {
            if (weaponObject.weapons[i].id == id)
            {
                weaponObject.weapons[i].isUnlocker = true;
                writeJson();
            }
        }
    }
}
