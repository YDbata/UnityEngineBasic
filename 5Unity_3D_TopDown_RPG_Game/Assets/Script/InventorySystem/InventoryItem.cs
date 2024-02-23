using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// inventory의 넣을 수 있는 모든 아이템을 나타내는 ScriptableObject입니다.
/// </summary>
public class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
{
    [Tooltip("저장 및 불러오기를 위한 자동 생성 UUID --> 새 UUID 생성하려면 이 필드를 지웁니다.")]
    [SerializeField] private string itemID = null;

    [Tooltip("UI에 표시된 아이템 이름")]
    [SerializeField] private string displayName = null;

    [Tooltip("UI에 표시될 아이템 설명")]
    [SerializeField][TextArea] string description = null;

    [Tooltip("inventory에서 이 아이템을 나타내는 UI Icon")]
    [SerializeField] private Sprite icon;

    /*[Tooltip("이 아이템이 드롭 될때 생성될 프리펩")]
    [SerializeField]*/

    [Tooltip("창고 슬롯에 여러 아이템을 쌓을 수 있는지 여부")]
    [SerializeField] private bool stackable = false;

    //상태
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
