using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder _inventoryHolder;
    [SerializeField] private InventorySlot_UI[] _slots;

    protected override void Start()
    {
        base.Start();

        if(_inventoryHolder != null)
        {
            _inventorySystem = _inventoryHolder.PrimaryInventorySystem;
            _inventorySystem.OnInventorySlotChange += UpdateSlot;
        }
        else
        {
            Debug.LogWarning($"No inventory assigned to {gameObject}");
        }

        AssignSlot(_inventorySystem);
    }

    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        _slotDictionary = new();

        if (_slots.Length != _inventorySystem.InventorySize) Debug.LogWarning($"Inventory slots out of sync on {gameObject}");

        for(int i = 0; i < _inventorySystem.InventorySize; i++)
        {
            _slotDictionary.Add(_slots[i], _inventorySystem.InventorySlots[i]);
            _slots[i].Init(_inventorySystem.InventorySlots[i]);
        }
    }
}
