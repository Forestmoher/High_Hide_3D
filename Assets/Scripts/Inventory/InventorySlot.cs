using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData _itemData;
    [SerializeField] private int _stackSize;

    public InventoryItemData ItemData => _itemData;
    public int StackSize => _stackSize;

    public InventorySlot(InventoryItemData source, int amount)
    {
        _itemData = source;
        _stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        _itemData = null;
        _stackSize = -1;
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        _itemData = data;
        _stackSize = amount;
    }

    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        _stackSize -= amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = _itemData.maxStackSize - _stackSize;
        return RoomLeftInStack(amountToAdd); 
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (_stackSize + amountToAdd <= _itemData.maxStackSize) return true;
        else return false;
    }

    public void AssignItem(InventorySlot slot)
    {
        if(_itemData == slot.ItemData) AddToStack(slot.StackSize);
        else
        {
            _itemData = slot.ItemData;
            _stackSize = 0;
            AddToStack(slot.StackSize);
        }
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if(_stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(_stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(ItemData, halfStack);
        return true;
    }
}
