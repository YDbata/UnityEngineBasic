using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickablePickUp : MonoBehaviour, IRaycastable
{
    PickUp pickUp;

    public CursorType GetCursorType()
    {
        return CursorType.Pickup;
    }

    public bool HandleRayCast(PlayerController callingController)
    {
        if(Input.GetMouseButtonDown(0))
        {
            pickUp.PickUpItem();
        }
        return true;
    }

    private void Awake()
    {
        pickUp = GetComponent<PickUp>();    
    }

    private void Update()
    {
        
    }

}
