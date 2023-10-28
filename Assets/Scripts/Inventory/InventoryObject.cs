using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();
    public List<WeaponSlot> weaponInventory = new List<WeaponSlot>();
    private GameObject tempDetectedWeapon;
    public GameObject TempDetectedWeapon => tempDetectedWeapon;
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

    public void AddWeapon(WeaponObject weapon, int amount)
    {
        bool hasWeapon = false;
        for (int i = 0; i < weaponInventory.Count; i++)
        {
            if (weaponInventory[i].weaponObject == weapon)
            {
                weaponInventory[i].amount += amount;
                hasWeapon = true;
                return;
            }
        }

        if (!hasWeapon)
        {
            weaponInventory.Add(new WeaponSlot(weapon, amount));
        }
    }

    public bool ContainsWeapon()
    {
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
    public int amount;

    public WeaponSlot(WeaponObject _weaponObject, int _amount)
    {
        this.weaponObject = _weaponObject;
        this.amount = _amount;
    }
}