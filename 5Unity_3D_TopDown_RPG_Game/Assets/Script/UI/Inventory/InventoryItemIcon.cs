using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// inventory item�� ��Ÿ���� icon�� ����
/// </summary>
[RequireComponent(typeof(Image))]
public class InventoryItemIcon : MonoBehaviour
{
    //���������� ����
    [SerializeField] GameObject textContainer = null;
    [SerializeField] TextMeshProUGUI itemNumber = null;

    /// <summary>
    /// item�� ����
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(InventoryItem item)
    {
        SetItem(item, 0);
    }


    /// <summary>
    /// item�� �������� ������ ����
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    public void SetItem(InventoryItem item, int number) 
    { 
        var iconImage = GetComponent<Image>();
        if (item == null)
        {
            iconImage.enabled = false;
        }
        else{
            iconImage.enabled = true;
            iconImage.sprite = item.GetIcon();
        }

        if (itemNumber)
        {
            if(number <= 1)
            {
                textContainer.SetActive(false);
            }
            else
            {
                textContainer.SetActive(true);
                itemNumber.text = number.ToString();
            }
        }
    }
}
