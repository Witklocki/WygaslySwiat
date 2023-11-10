using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();
    public List<WeaponSlot> weaponInventory = new();
    private GameObject tempDetectedWeapon;
    public GameObject TempDetectedWeapon => tempDetectedWeapon;

    public void Start()
    {
        weaponInventory.Clear();
        tempDetectedWeapon = null;
    }
    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].itemObject == _item)
            {
                container[i].AddAmount(_amount);
                hasItem = true;
                return;
            }
        }
        if (!hasItem)
        {
            container.Add(new InventorySlot(_item, _amount));
        }
    }

    public void AddWeapon(WeaponObject weapon)
    {
        for (int i = 0; i < weaponInventory.Count(); i++)
        {
            if (weaponInventory[i].weaponObject == weapon)
            {
                return;
            }
        }
        weaponInventory.Add(new WeaponSlot(weapon));
    }

    public bool ContainsWeapon()
    {
        Debug.Log(tempDetectedWeapon);

        return weaponInventory.Any();
    }

    public void ClearWeaponInventory()
    {
        weaponInventory.Clear();
    }
    public void SetLastDetectedWeapon(GameObject weaponObject)
    {
        tempDetectedWeapon = weaponObject;
    }
    public GameObject GetLastDetectedWeapon()
    {
        return tempDetectedWeapon;
    }
}


[System.Serializable]
public class InventorySlot
{
    public ItemObject itemObject;
    public int amount;
    private int maxAmount;

    public InventorySlot(ItemObject _itemObject,int _amount)
    {
        this.itemObject = _itemObject;
        this.amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
[System.Serializable]
public class WeaponSlot
{
    public WeaponObject weaponObject;

    public WeaponSlot(WeaponObject _weaponObject)
    {
        this.weaponObject = _weaponObject;
    }
}
