using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventorySlotsManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>(15);

    private void OnEnable()
    {
        InventoryManager.OnInventoryChange += DrawInventory;
    }

    private void OnDisable()
    {
        InventoryManager.OnInventoryChange -= DrawInventory;
    }

    void ResetInventory()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }

        inventorySlots = new List<InventorySlot>(15);
    }

    void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();

        for(int i = 0; i< inventorySlots.Capacity; i++)
        {
            CreateInventorySlot();
        }

        for(int i = 0; i<inventory.Count; i++)
        {
            inventorySlots[i].DrawSlot(inventory[i]);
        }
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();

        inventorySlots.Add(newSlotComponent);
    }
    
}
