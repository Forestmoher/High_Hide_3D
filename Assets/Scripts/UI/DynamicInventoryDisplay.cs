using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlot_UI _slotPrefab;
    protected override void Start()
    {
        base.Start();
    }

    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        ClearSlots();
        _inventorySystem = invToDisplay;
        if(_inventorySystem != null) _inventorySystem.OnInventorySlotChange += UpdateSlot;
        AssignSlot(invToDisplay);
    }

    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        _slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();
        if (inventoryToDisplay == null) return;

        for(int i = 0; i < inventoryToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(_slotPrefab, transform);
            _slotDictionary.Add(uiSlot, inventoryToDisplay.InventorySlots[i]);
            uiSlot.Init(inventoryToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }

    private void ClearSlots()
    {
        foreach(var item in transform.Cast<Transform>()) Destroy(item.gameObject);

        if (_slotDictionary != null) _slotDictionary.Clear();
    }

    private void OnDisable()
    {
        if(_inventorySystem != null) _inventorySystem.OnInventorySlotChange -= UpdateSlot;
    }
}
