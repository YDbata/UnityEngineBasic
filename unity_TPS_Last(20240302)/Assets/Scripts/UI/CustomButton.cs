using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    private UnityEvent _onDown;
    public UnityEvent OnDown
    {
        get { return _onDown; }
        set { _onDown = value; }
    }

    private UnityEvent _onUp;
    public UnityEvent OnUp { 
        get { return _onUp; }
        set { _onUp = value; } 
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _onDown?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        _onUp?.Invoke();
    }
}
