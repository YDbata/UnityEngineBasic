using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] InventoryItemIcon icon;

    // STATE
    int index;
    InventoryItem item;
    Inventory inventory;
    
    /// <summary>
    /// 인벤토리에서 해당 슬롯에 있는 아이템과 슬롯 인덱스를 받아오는 함수
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="index"></param>
    public void Setup(Inventory inventory, int index)
    {
        this.inventory = inventory;
        this.index = index;
        icon.SetItem(inventory.GetItemInSlot(index), inventory.GetNumberInSlot(index));
    }


}
