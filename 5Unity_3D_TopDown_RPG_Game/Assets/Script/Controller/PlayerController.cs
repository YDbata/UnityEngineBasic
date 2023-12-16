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
        public CursorType type; // Ŀ���� ����
        public Texture2D texture; // ������ ���� �̹���
        public Vector2 hotspot; // Ŀ���� ǥ�� ��ġ
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
        // 0 ��Ŭ, 1 ��Ŭ
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
        // SphereCast�� �������� ���� ����� ���� �Ǿ����� All�ϸ� ��� ��ü ��ȯ
        // raycastRadius : ���� ������
        RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
        float[] distance = new float[hits.Length];
        for(int i = 0; i < hits.Length; i++)
        {
            distance[i] = hits[i].distance;
        }
        // distance�� �������� hits�� ����� ������ ����
        Array.Sort(distance, hits);
        return hits;
        
    }

    private bool InteractWithMovement()
    {
        // �÷��̾ ������ target ��ġ
        Vector3 target;
        // �÷��̾ Ŭ���� ���� ��ǥ�� ��濡 �ִ��� Ȯ���ϴ� ����
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
        // ���� ������Ʈ�� �ִ����� Ȯ���ϴ� �ڵ�
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if(!hasHit) { return false; }

        //NavMesh�� ����ִ� ��ġ�� ��ȯ�� �� �ִ��� Ȯ���ϴ� �ڵ�
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
    // region ���� �������� �����
    #region Ŀ������ �ڵ�

    /// <summary>
    /// Ŀ�� ���� texture
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
