using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] InventorySlotUI inventoryItemPrefab = null;

    Inventory playerInventory;

    private void Awake()
    {
        playerInventory = Inventory.GetPlayerInventory();
        playerInventory.inventoryUpdated += ReDraw;
    }

    private void Start()
    {
        ReDraw();
    }

    private void ReDraw()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < playerInventory.GetSize(); i++)
        {
            var itemUI = Instantiate(inventoryItemPrefab, transform);
            itemUI.Setup(playerInventory, i);
        }
    }
}
