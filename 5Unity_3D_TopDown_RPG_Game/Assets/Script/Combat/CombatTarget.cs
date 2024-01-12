using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTarget : MonoBehaviour, IRaycastable
{
    public CursorType GetCursorType()
    {
        return CursorType.Combat;
    }

    public bool HandleRayCast(PlayerController callingController)
    {

        // enabled : MonoBehaviour를 사용할 수 있는지 확인
        // game오브젝트가 꺼져있거나 하면 사용할 수 없어서 return false
        if (!enabled) return false;
        if (!CatchComponent(callingController)) return false;
        if(Input.GetMouseButtonDown(0))
        {
            CatchComponentToAttack(callingController);
        }
        return true;
    }


    public bool CatchComponent(PlayerController callingController)
    {
        var fighter = callingController.GetComponent<Fighter>();
        if (fighter)
        {
            return fighter.CanAttack(this.gameObject);
        }
        return false;
    }
    private void CatchComponentToAttack(PlayerController callingController)
    {
        var fighter = callingController.GetComponent<Fighter>();
        if(fighter)
        {
            fighter.Attack(this.gameObject);
            return;
        }
    }

   
}
