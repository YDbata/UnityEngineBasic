using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    /// <summary>
    /// 넘겨줄 인벤토리아이템
    /// </summary>
    [SerializeField] InventoryItem item;
    /// <summary>
    /// 아이템 개수
    /// </summary>
    int number = 1;

    Inventory inventory;

    private void Awake()
    {
        inventory = Inventory.GetPlayerInventory();
    }

    public void PickUpItem()
    {
        bool foundSlot = inventory.AddToFirstEmptySlot(item, number);
        if (foundSlot)
        {
            Destroy(gameObject);
        }

    }
}
