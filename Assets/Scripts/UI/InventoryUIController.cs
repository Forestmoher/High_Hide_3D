using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay backpackPanel;

    private void Awake()
    {
        chestPanel.gameObject.SetActive(false);
        backpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnBackpackInventoryDisplayRequested += DisplayBackpack;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnBackpackInventoryDisplayRequested -= DisplayBackpack;
    }

    void Update()
    {
        if(chestPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) chestPanel.gameObject.SetActive(false);
        if(backpackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) backpackPanel.gameObject.SetActive(false);
    }

    private void DisplayInventory(InventorySystem invToDisplay)
    {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);
    }

    private void DisplayBackpack(InventorySystem invToDisplay)
    {
        backpackPanel.gameObject.SetActive(true);
        backpackPanel.RefreshDynamicInventory(invToDisplay);
    }
}
