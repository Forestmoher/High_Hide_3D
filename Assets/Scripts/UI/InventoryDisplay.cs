using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private MouseItemData _mouseInventoryItem;

    protected InventorySystem _inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> _slotDictionary;

    public InventorySystem InventorySystem => _inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => _slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem inventoryToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach(var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot) //slot value - the 'back end inventory slot
            {
                slot.Key.UpdateUISlot(updatedSlot);//slot key - UI slot representing backend slot
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;

        //if the clicked has an item, and mouse does not - pick up item
        if(clickedUISlot.AssignedInventorySlot.ItemData != null && _mouseInventoryItem.assignedInventorySlot.ItemData == null)
        {
            //if player is holding shift - split stack
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                _mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            else
            {
                _mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }

        }

        //clicked slot doesn't have item, but mouse does - place mouse item into slot
        if(clickedUISlot.AssignedInventorySlot.ItemData == null && _mouseInventoryItem.assignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.assignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            _mouseInventoryItem.assignedInventorySlot.ClearSlot();
            _mouseInventoryItem.ClearSlot();
            return;
        }
        //both slots have an item - decide what to do here
        if(clickedUISlot.AssignedInventorySlot.ItemData != null && _mouseInventoryItem.assignedInventorySlot.ItemData != null)
        {
            //if items are the same
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == _mouseInventoryItem.assignedInventorySlot.ItemData;
            //and there is room left in stack
            if (isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(_mouseInventoryItem.assignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(_mouseInventoryItem.assignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                _mouseInventoryItem.ClearSlot();
                return;
            }
            //else get what is remaining in mouse stack
            else if (isSameItem && !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(_mouseInventoryItem.assignedInventorySlot.StackSize, out int leftInStack))
            {
                //stack is full, so swap items
                if (leftInStack < 1)
                {
                    SwapSlots(clickedUISlot);
                    return;
                } 
                else
                {
                    //get how many are left in mouse stack 
                    int remainingOnMouse = _mouseInventoryItem.assignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(_mouseInventoryItem.assignedInventorySlot.ItemData, remainingOnMouse);
                    _mouseInventoryItem.ClearSlot();
                    _mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }
            //if items are different
            else if(!isSameItem)
            {
                SwapSlots(clickedUISlot);
                return;
            }
        }
    }

    private void SwapSlots(InventorySlot_UI clickedSlot)
    {
        var clonedSlot = new InventorySlot(_mouseInventoryItem.assignedInventorySlot.ItemData, _mouseInventoryItem.assignedInventorySlot.StackSize);
        _mouseInventoryItem.ClearSlot();

        _mouseInventoryItem.UpdateMouseSlot(clickedSlot.AssignedInventorySlot);

        clickedSlot.ClearSlot();
        clickedSlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedSlot.UpdateUISlot();
    }
}
