using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 5f; // �÷��̾� ���� ���� �Ÿ�
    [SerializeField] private float suspicionTime = 3f; // �÷��̾ �ǽ�(�ǽ�)�ϴ� �ð�
    [SerializeField] private float agroCooldownTime = 5f; // ��һ��·� ���ư��� cool �ð�
    [SerializeField] private PatrolPath patrolPath; // ���� ���
    [SerializeField] private float wayPointToLerance = 1f; // ���� ���� ���� ��� �Ÿ�
    [SerializeField] private float waypointDwellTime = 3f; // �������� ���� �� ��� �ð�

    [Range(0f, 1f)]
    [SerializeField] private float patrolSpeedFraction = 0.2f; //���� �ӵ� ����
    [SerializeField] private float shoutDistance = 5f; // �ֺ� ������ �˷��ִ� ����

    Fighter fighter; // ���ݴ��
    Health health; // ü�� ���
    Mover mover; //�̵� ���
    GameObject player; // �÷��̾�(Ÿ��)


    LazyValue<Vector3> guardPosition; // AI�� ��� ��ġ
    float timeSinceLastSawPlayer = Mathf.Infinity; // ���������� �÷��̾� ��� �ð�
    float timeSinceArrivedAtWaypoint = Mathf.Infinity; // ���� ������ ������ �ð�
    float timeSinceAggrevated = Mathf.Infinity; // ���ݻ��·� ��ȯ�� �� ��� �ð�
    int currentWaypointIndex = 0; // ���� ���� ��� �ε���

    private void Awake()
    {
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
        mover = GetComponent<Mover>();
        player = GameObject.FindWithTag("Player");

        guardPosition = new LazyValue<Vector3>(GetguardPosition);
    }

    private Vector3 GetguardPosition()
    {
        return transform.position;
    }

    private void Start()
    {
        guardPosition.ForceInit(); //�����ġ �ʱ�ȭ

    }
    private void Update()
    {
        if (health.IsDead()) return;

        // ��׷� �����̰� ������ �ִٸ�
        if (IsAggrevated() && fighter.CanAttack(player))
        {
            // ����!
            AttackBehavior();
        }
        // 
        else if(timeSinceLastSawPlayer < suspicionTime)
        {
            SuspicionBehaviour(); //�ǽ�
        }
        else
        {
            patrolBehaviour(); //����
        }

        UpdateTimers();

    }

    private void UpdateTimers()
    {
        timeSinceAggrevated += Time.deltaTime;
        timeSinceArrivedAtWaypoint += Time.deltaTime;
        timeSinceLastSawPlayer += Time.deltaTime;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void patrolBehaviour()
    {
        Vector3 nextPosition = guardPosition.value;
        if(patrolPath != null)
        {
            // ��ǥ������ �����ߴ��� Ȯ��
            if (AtWaypoint())
            {
                // ������ ���������� �ʱ�ȭ �� ���� ����Ʈ ������
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }

        // ��ǥ������ ���� �� ����� �ð��� ���� ��� �ð����� ũ�ٸ�
        // ������������ �̵���Ų��.
        if(timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }
    }

    /// <summary>
    /// ���� ���� �������� ��ȯ
    /// </summary>
    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    /// <summary>
    /// ���������� �����ߴ��� Ȯ��
    /// </summary>
    /// <returns></returns>
    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < wayPointToLerance;
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    /// <summary>
    /// �ǽ��ϴ� ����
    /// </summary>
    private void SuspicionBehaviour()
    {
        GetComponent<ActionScheduler>().CancleCurrentAction();
    }

    /// <summary>
    /// ���ݻ��¿��� �������� ��ȯ
    /// </summary>
    private void AttackBehavior()
    {
        timeSinceLastSawPlayer = 0;
        fighter.Attack(player);


    }

    /// <summary>
    /// �ֺ��� �ٸ� AI�� ���� ���·� ��ȯ ��Ŵ
    /// </summary>
    private void aggrevateNearbyEnemies()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);
        foreach (RaycastHit hit in hits)
        {
            AIController ai = hit.collider.GetComponent<AIController>();
            if(ai == null) continue;
            ai.Aggrevate();
        }
    }

    /// <summary>
    /// AI�� ���ݻ��·� ��ȯ(��׷��� ����)
    /// </summary>
    private void Aggrevate()
    {
        timeSinceAggrevated = 0;
    }

    /// <summary>
    /// �÷��̾�� ��׷� �������� Ȯ��
    /// </summary>
    /// <returns>
    ///     true ����
    ///     false �Ұ���
    /// </returns>
    private bool IsAggrevated()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // �Ÿ��� �����ų�, ��׷� �ð��� ������ �ʾ��� �� ���� ���� ����
        return distanceToPlayer < chaseDistance || timeSinceAggrevated < agroCooldownTime;
    }
}
