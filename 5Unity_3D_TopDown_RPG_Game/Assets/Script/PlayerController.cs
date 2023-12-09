using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float maxNavMeshProjectionDistance = 1;
    [SerializeField] private Mover mover;
    private Vector3 _inputVec;


    private void Awake()
    {
        mover = GetComponent<Mover>();
        
    }

    private void Update()
    {
        InteractWithMovement();
        
    }

    private void InteractWithMovement()
    {
        // 플레이어가 움직일 target 위치
        Vector3 target;
        // 플레이어가 클릭한 곳의 좌표가 배경에 있는지 확인하는 변수
        bool hasHit = RaycastNavMesh(out target);
        if (hasHit)
        {
            if(!mover.CanMoveTo(target))
                return;
            if (Input.GetMouseButtonDown(0))
            {
                mover.MoveTo(target, 1f);
            }
        }
    }

    private bool RaycastNavMesh(out Vector3 target)
    {
        target = Vector3.zero;
        // 게임 오브젝트가 있는지를 확인하는 코드
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if(!hasHit) { return false; }

        //NavMesh가 깔려있는 위치를 반환할 수 있는지 확인하는 코드
        NavMeshHit navMeshHit;
        bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
        if(!hasCastToNavMesh)
        {
            return false;
        }

        target = navMeshHit.position;
        return true;

    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
