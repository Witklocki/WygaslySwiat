using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

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