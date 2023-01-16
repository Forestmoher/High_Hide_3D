using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MouseItemData : MonoBehaviour
{
    [Header("Inventory")]
    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public InventorySlot assignedInventorySlot;
    [SerializeField] private CursorController _cursorController;

    private void Awake()
    {
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot slot)
    {
        assignedInventorySlot.AssignItem(slot);
        itemSprite.sprite = slot.ItemData.itemIcon;
        itemCount.text = slot.StackSize.ToString();
        itemSprite.color = Color.white;
    }

    private void Update()
    {
        if(assignedInventorySlot.ItemData != null)
        {
            transform.position = _cursorController.transform.position;

            if(Input.GetMouseButtonDown(0) && !IsPointerOverUIObject(gameObject))
            {
                ClearSlot();
            }
        }
    }

    public void ClearSlot()
    {
        assignedInventorySlot.ClearSlot();
        itemCount.text = "";
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;
    }

    public static bool IsPointerOverUIObject(GameObject mouse)
    {
        PointerEventData eventDataCurrentPosition = new(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
