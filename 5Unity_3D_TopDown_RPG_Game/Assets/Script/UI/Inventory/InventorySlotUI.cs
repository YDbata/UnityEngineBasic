using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
{
    [SerializeField] InventoryItemIcon icon;

    // STATE
    int index;
    InventoryItem item;
    Inventory inventory;

    /// <summary>
    /// �κ��丮���� �ش� ���Կ� �ִ� �����۰� ���� �ε����� �޾ƿ��� �Լ�
    /// </summary>
    /// <param name="inventory"></param>
    /// <param name="index"></param>
    public void Setup(Inventory inventory, int index)
    {
        this.inventory = inventory;
        this.index = index;
        icon.SetItem(inventory.GetItemInSlot(index), inventory.GetNumberInSlot(index));
    }

    public void AddItems(InventoryItem item, int number)
    {
        // �κ��丮�� �������� �߰��ϴ� �ڵ�
        inventory.AddItemToSlot(index, item, number);
    }

    public InventoryItem GetItem()
    {
        return inventory.GetItemInSlot(index);
    }

    public int GetNumber()
    {
        return inventory.GetNumberInSlot(index);
    }

    public int MaxAcceptable(InventoryItem item)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveItems(int number)
    {
        throw new System.NotImplementedException();
    }

}
