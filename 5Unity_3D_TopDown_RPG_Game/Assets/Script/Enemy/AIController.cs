using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private float chaseDistance = 5f; // 플레이어 추적 시작 거리
    [SerializeField] private float suspicionTime = 3f; // 플레이어를 의심(의식)하는 시간
    [SerializeField] private float agroCooldownTime = 5f; // 평소상태로 돌아가는 cool 시간
    [SerializeField] private PatrolPath patrolPath; // 순찰 경로
    [SerializeField] private float wayPointToLerance = 1f; // 순찰 지점 도달 허용 거리
    [SerializeField] private float waypointDwellTime = 3f; // 순찰지점 도착 후 대기 시간

    [Range(0f, 1f)]
    [SerializeField] private float patrolSpeedFraction = 0.2f; //순찰 속도 비율
    [SerializeField] private float shoutDistance = 5f; // 주변 적에게 알려주는 범위

    Fighter fighter; // 공격담당
    Health health; // 체력 담당
    Mover mover; //이동 담당
    GameObject player; // 플레이어(타겟)


    LazyValue<Vector3> guardPosition; // AI의 경계 위치
    float timeSinceLastSawPlayer = Mathf.Infinity; // 마지막으로 플레이어 목격 시간
    float timeSinceArrivedAtWaypoint = Mathf.Infinity; // 순찰 지점에 도달한 시간
    float timeSinceAggrevated = Mathf.Infinity; // 공격상태로 전환한 후 경과 시간
    int currentWaypointIndex = 0; // 현재 순찰 경로 인덱스

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
        guardPosition.ForceInit(); //경계위치 초기화

    }
    private void Update()
    {
        if (health.IsDead()) return;

        // 어그로 상태이고 때릴수 있다면
        if (IsAggrevated() && fighter.CanAttack(player))
        {
            // 공격!
            AttackBehavior();
        }
        // 
        else if(timeSinceLastSawPlayer < suspicionTime)
        {
            SuspicionBehaviour(); //의심
        }
        else
        {
            patrolBehaviour(); //순찰
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
    /// 순찰
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void patrolBehaviour()
    {
        Vector3 nextPosition = guardPosition.value;
        if(patrolPath != null)
        {
            // 목표지점에 도달했는지 확인
            if (AtWaypoint())
            {
                // 지점에 도달했으면 초기화 후 다음 포인트 가져옴
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }

        // 목표지점에 도착 후 경과한 시간이 설정 대기 시간보다 크다면
        // 다음지점으로 이동시킨다.
        if(timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }
    }

    /// <summary>
    /// 다음 순찰 지점으로 순환
    /// </summary>
    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    /// <summary>
    /// 순찰지점에 도달했는지 확인
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
    /// 의심하는 동작
    /// </summary>
    private void SuspicionBehaviour()
    {
        GetComponent<ActionScheduler>().CancleCurrentAction();
    }

    /// <summary>
    /// 공격상태에서 공격으로 전환
    /// </summary>
    private void AttackBehavior()
    {
        timeSinceLastSawPlayer = 0;
        fighter.Attack(player);


    }

    /// <summary>
    /// 주변에 다른 AI를 공격 상태로 전환 시킴
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
    /// AI를 공격상태로 변환(어그로의 시작)
    /// </summary>
    private void Aggrevate()
    {
        timeSinceAggrevated = 0;
    }

    /// <summary>
    /// 플레이어에게 어그로 상태인지 확인
    /// </summary>
    /// <returns>
    ///     true 가능
    ///     false 불가능
    /// </returns>
    private bool IsAggrevated()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // 거리가 가깝거나, 어그로 시간이 끝나지 않았을 때 공격 상태 유지
        return distanceToPlayer < chaseDistance || timeSinceAggrevated < agroCooldownTime;
    }
}
