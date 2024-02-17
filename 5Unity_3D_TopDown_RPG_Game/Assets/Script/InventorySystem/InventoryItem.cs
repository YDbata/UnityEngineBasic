using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// inventory�� ���� �� �ִ� ��� �������� ��Ÿ���� ScriptableObject�Դϴ�.
/// </summary>
public class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
{
    [Tooltip("���� �� �ҷ����⸦ ���� �ڵ� ���� UUID --> �� UUID �����Ϸ��� �� �ʵ带 ����ϴ�.")]
    [SerializeField] private string itemID = null;

    [Tooltip("UI�� ǥ�õ� ������ �̸�")]
    [SerializeField] private string displayName = null;

    [Tooltip("UI�� ǥ�õ� ������ ����")]
    [SerializeField][TextArea] string description = null;

    [Tooltip("inventory���� �� �������� ��Ÿ���� UI Icon")]
    [SerializeField] private Sprite icon;

    /*[Tooltip("�� �������� ��� �ɶ� ������ ������")]
    [SerializeField]*/

    [Tooltip("â�� ���Կ� ���� �������� ���� �� �ִ��� ����")]
    [SerializeField] private bool stackable = false;

    //����
    static Dictionary<string, InventoryItem> itemLookupCache;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeforeSerialize()
    {
        if (string.IsNullOrWhiteSpace(itemID))
        {
            itemID = System.Guid.NewGuid().ToString();
        }
    }

    public void OnAfterDeserialize()
    {
        
    }

    public bool IsStackable() => stackable;

    public Sprite GetIcon() => icon;

    public string GetDisplayName() => displayName;

    public string GETDescription() => description;
}
