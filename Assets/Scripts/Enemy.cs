using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask targetLayer;

    public EnemySO Data;

    public EnemyAnimData animData;

    public Animator animator { get; private set; }
    public CharacterController Controller { get; private set; }
    private EnemyStateMachine stateMachine;

    // 공격 딜레이 카운터
    float atkDelayCount = 0f;
    // 생사여부
    bool isDead = false;
    // 현재 체력
    float HPCurrent;

    private void Awake()
    {
        animData.Initialize();
        animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
        {
            skinnedMeshRenderer.material.color *= Color.red; 
        }

        // 현재 체력을 최대 체력으로
        HPCurrent = stateMachine.Enemy.Data.BattleData.HPMax;
    }

    private void Update()
    {
        // 죽었다면 더 연산하지 않도록
        if (isDead)
            return;

        stateMachine.Update();

        // 타겟이 공격 범위 안에 있다면
        if (stateMachine.target.distance < stateMachine.Enemy.Data.GroundData.AtkableRange)
        {
            // 카운트를 세어주다
            atkDelayCount += Time.deltaTime;
            // 공격 딜레이가 지났다면
            if(atkDelayCount >= stateMachine.Enemy.Data.BattleData.AtkDelay)
            {
                atkDelayCount = 0;
                // 공격 모션 재생
                stateMachine.ChangeState(stateMachine.AtkState);
                return;
            }
            // 공격 딜레이 동안 대기 모션
            // 트랜지션 도중이 아니고, 대기 모션이 아니라면
            else if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateMachine.Enemy.animData.IdleParameterHash)
            {
                // 대기
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
        // 타겟이 공격 범위 ~ 탐색 범위 안에 있다면
        else if(stateMachine.target.distance <= stateMachine.Enemy.Data.GroundData.DetectRange)
        {
            stateMachine.ChangeState(stateMachine.RunState);
            return;
        }
        // 플레이어가 탐색 범위 내에 없다면, idle로 변경
        // 트랜지션 도중이 아니고, 대기 모션이 아니라면
        else if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateMachine.Enemy.animData.IdleParameterHash)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }

    void FixedUpdate()
    {
        stateMachine.target = DetectTarget();
        stateMachine.PhysicsUpdate();
    }

    DetectInfo DetectTarget()
    {
        float detectRange = stateMachine.Enemy.Data.GroundData.DetectRange;
        Collider[] player = Physics.OverlapSphere(transform.position, detectRange, targetLayer);
        if (player.Length != 0)
        {
            // 만약 플레이어가 여럿이 생긴다면 위를 지우고 아래 구문에서 가장 가까운 적을 타겟으로
            int minIndex = 0;
            float distMin = detectRange;
            for (int i = 0; i < player.Length; i++)
            {
                float distTmp = Vector3.Distance(transform.position, player[i].transform.position);
                if (distTmp < distMin)
                {
                    distMin = distTmp;
                    minIndex = i;
                }
            }
            Vector3 dir = (player[minIndex].transform.position - transform.position).normalized;
            return new DetectInfo(player[minIndex].transform, distMin, dir);
        }

        return new DetectInfo(null, float.MaxValue, Vector3.zero);
    }

    // 대미지 받음 및 사망 처리
    public bool GetDamage(float damage)
    {
        // 죽으면 중복으로 들어가지 않게끔.. 하려했는데 
        if (isDead) return true;

        HPCurrent -= damage;
        if (HPCurrent <= 0)
        {
            // 다시 타겟으로 잡히지 않게끔 레이어 변경
            this.gameObject.layer = LayerMask.GetMask("Default");
            // 콜라이더도 꺼주게끔
            Controller.enabled = false;
            HPCurrent = 0;
            isDead = true;
            stateMachine.ChangeState(stateMachine.DeathState);

            return true;
        }
        return false;
    }
}

// 타겟과의 거리/방향
public class DetectInfo
{
    public Transform targetTransform;
    public float distance;
    public Vector3 direction;

    public DetectInfo(Transform target, float distance, Vector3 direction)
    {
        this.targetTransform = target;
        this.distance = distance;
        this.direction = direction;
    }
}
