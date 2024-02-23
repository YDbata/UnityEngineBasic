using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Serializable]
    struct CursorMapping
    {
        public CursorType type; // 커서의 종류
        public Texture2D texture; // 종류에 따른 이미지
        public Vector2 hotspot; // 커서의 표시 위치
    }
    [SerializeField] private CursorMapping[] cursorMappings = null;
    [SerializeField] private float maxNavMeshProjectionDistance = 1;
    [SerializeField] private float raycastRadius = 1;
    
    
    [SerializeField] private Mover mover;
    private Vector3 _inputVec;

    private ActionScheduler _actionScheduler;

    private void Awake()
    {
        mover = GetComponent<Mover>();
        _actionScheduler = GetComponent<ActionScheduler>();
    }

    private void Update()
    {   
        // 0 좌클, 1 우클
        if(Input.GetMouseButtonDown(1))
            _actionScheduler.CancleCurrentAction();
        if (InteractWithComponent()) return;
        if (InteractWithMovement()) return;
        SetCursor(CursorType.None);
        
    }

    private bool InteractWithComponent()
    {
        RaycastHit[] hits = RaycastAllSorted();
        foreach (RaycastHit hit in hits)
        {
            IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
            foreach (IRaycastable raycastable in raycastables)
            {
                if (raycastable.HandleRayCast(this))
                {
                    SetCursor(raycastable.GetCursorType());
                    return true;
                }
            }
        }
        return false;
    }

    private RaycastHit[] RaycastAllSorted()
    {
        // SphereCast는 레이저의 끝이 공모양 구로 되어있음 All하면 모든 물체 반환
        // raycastRadius : 공의 반지름
        RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
        float[] distance = new float[hits.Length];
        for(int i = 0; i < hits.Length; i++)
        {
            distance[i] = hits[i].distance;
        }
        // distance를 기준으로 hits를 가까운 순으로 정렬
        Array.Sort(distance, hits);
        return hits;
        
    }

    private bool InteractWithMovement()
    {
        // 플레이어가 움직일 target 위치
        Vector3 target;
        // 플레이어가 클릭한 곳의 좌표가 배경에 있는지 확인하는 변수
        bool hasHit = RaycastNavMesh(out target);
        if (hasHit)
        {
            if(!mover.CanMoveTo(target))
                return false;
            if (Input.GetMouseButtonDown(0))
            {
                mover.StartMoveAction(target, 1f);
            }
            SetCursor(CursorType.Movement);
            return true;
        }
        return false;
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
    // region 폴더 정리관련 예약어
    #region 커서변경 코드

    /// <summary>
    /// 커서 설정 texture
    /// </summary>
    /// <param name="type"></param>
    private void SetCursor(CursorType type)
    {
        CursorMapping mapping = GetCursorMapping(type);
        Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
    }

    private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach(CursorMapping mapping in cursorMappings)
        {
            if (mapping.type == type)
            {
                return mapping;
            }
        }
        return cursorMappings[0];
    }


    #endregion
}
