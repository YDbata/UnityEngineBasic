using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// player Inventory에 대한 저장 공간을 제공하는 코드, 구성가능한 개수의 슬롯 사용
/// </summary>
public class Inventory : MonoBehaviour
{
    [Tooltip("허용된 인벤토리 크기")]
    [SerializeField] private int inventorySize = 16;

    [SerializeField] private InventorySlot[] slots;


    [Serializable] public struct InventorySlot
    {
        public InventoryItem item;
        public int number;
    }

    public event Action inventoryUpdated;

    private void Awake()
    {
        slots = new InventorySlot[inventorySize];
    }

    /// <summary>
    /// player GameObject에서 inventory Component를 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public static Inventory GetPlayerInventory()
    {
        var player = GameObject.FindWithTag("Player");
        return player.GetComponent<Inventory>();
    }

    /// <summary>
    /// item을 첫번째 사용 가능한 슬롯에 추가를 시도
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public bool AddToFirstEmptySlot(InventoryItem item, int number)
    {
        int i = FindSlot(item);
        if(i < 0)
        {
            return false;
        }
        slots[i].item = item;
        slots[i].number += 1;
        inventoryUpdated?.Invoke();
        return true;
        
    }

    /// <summary>
    /// 주어진 item을 수용할 수 있는 Slot 찾기
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private int FindSlot(InventoryItem item)
    {
        // 쌓을 수 있는 슬롯을 먼저 찾는다.
        int i = FindStack(item);
        if(i < 0)
        {
            i = FindEmptySlot();
        }
        return i;
    }

    /// <summary>
    /// 비어있는 Slot을 찾는다.
    /// </summary>
    /// <returns></returns>
    private int FindEmptySlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 이 아이템 유형에 기존 스택(아이템)을 찾는다.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private int FindStack(InventoryItem item)
    {
        if (item.IsStackable())
        {
            return -1;
        }
        for(int i = 0;i < slots.Length;i++)
        {

            if (object.ReferenceEquals(slots[i].item, item))
            {
                return i;
            }
        }
        return -1;
    }

    public InventoryItem GetItemInSlot(int index)
    {
        return slots[index].item;        
    }

    public int GetNumberInSlot(int index)
    {
        return slots[index].number;       
    }

    public int GetSize() => slots.Length;
}
