using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int _secondaryInventorySize;
    [SerializeField] protected InventorySystem _secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => _secondaryInventorySystem;
    public static UnityAction<InventorySystem> OnBackpackInventoryDisplayRequested;


    protected override void Awake()
    {
        base.Awake();

        _secondaryInventorySystem = new InventorySystem(_secondaryInventorySize);
    }

    void Update()
    {
        if(Keyboard.current.tabKey.wasPressedThisFrame) OnBackpackInventoryDisplayRequested?.Invoke(SecondaryInventorySystem);
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if(_primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        else if(_secondaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        return false;
    }
}
