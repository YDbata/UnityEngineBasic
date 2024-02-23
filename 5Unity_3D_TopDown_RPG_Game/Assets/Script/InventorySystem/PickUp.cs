using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    /// <summary>
    /// �Ѱ��� �κ��丮������
    /// </summary>
    [SerializeField] InventoryItem item;
    /// <summary>
    /// ������ ����
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
