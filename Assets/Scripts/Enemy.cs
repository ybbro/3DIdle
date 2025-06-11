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

    // ���� ������ ī����
    float atkDelayCount = 0f;
    // ���翩��
    bool isDead = false;
    // ���� ü��
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

        // ���� ü���� �ִ� ü������
        HPCurrent = stateMachine.Enemy.Data.BattleData.HPMax;
    }

    private void Update()
    {
        // �׾��ٸ� �� �������� �ʵ���
        if (isDead)
            return;

        stateMachine.Update();

        // Ÿ���� ���� ���� �ȿ� �ִٸ�
        if (stateMachine.target.distance < stateMachine.Enemy.Data.GroundData.AtkableRange)
        {
            // ī��Ʈ�� �����ִ�
            atkDelayCount += Time.deltaTime;
            // ���� �����̰� �����ٸ�
            if(atkDelayCount >= stateMachine.Enemy.Data.BattleData.AtkDelay)
            {
                atkDelayCount = 0;
                // ���� ��� ���
                stateMachine.ChangeState(stateMachine.AtkState);
                return;
            }
            // ���� ������ ���� ��� ���
            // Ʈ������ ������ �ƴϰ�, ��� ����� �ƴ϶��
            else if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateMachine.Enemy.animData.IdleParameterHash)
            {
                // ���
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
        // Ÿ���� ���� ���� ~ Ž�� ���� �ȿ� �ִٸ�
        else if(stateMachine.target.distance <= stateMachine.Enemy.Data.GroundData.DetectRange)
        {
            stateMachine.ChangeState(stateMachine.RunState);
            return;
        }
        // �÷��̾ Ž�� ���� ���� ���ٸ�, idle�� ����
        // Ʈ������ ������ �ƴϰ�, ��� ����� �ƴ϶��
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
            // ���� �÷��̾ ������ ����ٸ� ���� ����� �Ʒ� �������� ���� ����� ���� Ÿ������
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

    // ����� ���� �� ��� ó��
    public bool GetDamage(float damage)
    {
        // ������ �ߺ����� ���� �ʰԲ�.. �Ϸ��ߴµ� 
        if (isDead) return true;

        HPCurrent -= damage;
        if (HPCurrent <= 0)
        {
            // �ٽ� Ÿ������ ������ �ʰԲ� ���̾� ����
            this.gameObject.layer = LayerMask.GetMask("Default");
            // �ݶ��̴��� ���ְԲ�
            Controller.enabled = false;
            HPCurrent = 0;
            isDead = true;
            stateMachine.ChangeState(stateMachine.DeathState);

            return true;
        }
        return false;
    }
}

// Ÿ�ٰ��� �Ÿ�/����
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
