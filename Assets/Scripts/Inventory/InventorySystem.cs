using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> _inventorySlots;
    public List<InventorySlot> InventorySlots => _inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChange;

    public InventorySystem(int size)
    {
        _inventorySlots = new List<InventorySlot>(size);

        for(int i = 0; i < size; i++)
        {
            _inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if(ContainsItem(itemToAdd, out List<InventorySlot> slotList)) //check if item already exists in inventory
        {
            foreach(var slot in slotList)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChange?.Invoke(slot);
                    return true;
                }
            }
        }
        
        if(HasFreeSlot(out InventorySlot freeSlot)) //gets first available slot
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChange?.Invoke(freeSlot);
            return true;
        }
        //if no free space
        return false;
    }

    //get list of slots that contain an item
    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> slotList)
    {
        slotList = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();

        return slotList != null;
    }

    //check if there is an empty slot in inventory syste
    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot != null;
    }
}
