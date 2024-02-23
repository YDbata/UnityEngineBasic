using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRaycastable
{
    CursorType GetCursorType();

    bool HandleRayCast(PlayerController callingController);
}
