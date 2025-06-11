using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask targetLayer;

    public PlayerSO Data;

    public PlayerAnimData animData;

    [SerializeField] PlayerHPBar HPBar;

    public Animator animator { get; private set; }
    public PlayerController Input { get; private set; }
    public CharacterController Controller { get; private set; }

    private PlayerStateMachine stateMachine;

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

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        // ���� ü���� �ִ� ü������
        HPCurrent = stateMachine.Player.Data.BattleData.HPMax;
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        // �׾��ٸ� �� �������� �ʵ���
        if (isDead)
            return;

        stateMachine.Update();

        // Ÿ���� ���� ���� �ȿ� �ִٸ�
        if (stateMachine.target.distance < stateMachine.Player.Data.GroundData.AtkableRange)
        {
            // ī��Ʈ�� �����ִ�
            atkDelayCount += Time.deltaTime;
            // ���� �����̰� �����ٸ�
            if (atkDelayCount >= stateMachine.Player.Data.BattleData.AtkDelay)
            {
                atkDelayCount = 0;
                // ���� ��� ���
                stateMachine.ChangeState(stateMachine.AtkState);
                return;
            }
            // ���� ������ ���� ��� ���
            // Ʈ������ ������ �ƴϰ�, ��� ����� �ƴ϶��
            else if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateMachine.Player.animData.IdleParameterHash)
            {
                // ���
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
        // Ÿ���� ���� ���� ~ Ž�� ���� �ȿ� �ִٸ�
        else if (stateMachine.target.distance <= stateMachine.Player.Data.GroundData.DetectRange)
        {
            stateMachine.ChangeState(stateMachine.RunState);
            return;
        }
        // �÷��̾ Ž�� ���� ���� ���ٸ�, idle�� ����
        // Ʈ������ ������ �ƴϰ�, ��� ����� �ƴ϶��
        else if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateMachine.Player.animData.IdleParameterHash)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        // ��ǲ�� ��� �׿������ >> �ð��� ���� ���´ٸ� �ڵ� Ǯ�� ������ �ְԲ�
        // stateMachine.HandleInput();
    }

    private void FixedUpdate()
    {
        stateMachine.target = DetectTarget();
        stateMachine.PhysicsUpdate();
    }

    // ����� ���� �� ��� ó��
    public void GetDamage(float damage)
    {
        HPCurrent -= damage;
        if (HPCurrent <= 0)
        {
            HPCurrent = 0;
            isDead = true;
            stateMachine.ChangeState(stateMachine.DeathState);
        }
        // ü�¹� ��ȭ�� ǥ��
        HPBar.ChangeHPBar(HPCurrent, stateMachine.Player.Data.BattleData.HPMax);
    }

    public void Heal(float HealAmount)
    {
        float hpMax = stateMachine.Player.Data.BattleData.HPMax;
        HPCurrent = Mathf.Min(HPCurrent + HealAmount, hpMax);
        // ü�¹� ��ȭ�� ǥ��
        HPBar.ChangeHPBar(HPCurrent, hpMax);
    }

    public void AtkChange(float amount)
    {
        float atk = stateMachine.Player.Data.BattleData.Atk;
        atk = Mathf.Max(atk + amount, 0);
        stateMachine.Player.Data.BattleData.Atk = atk;
    }

    DetectInfo DetectTarget()
    {
        if (stateMachine.target.targetTransform == null)
        {
            float detectRange = stateMachine.Player.Data.GroundData.DetectRange;
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
        }
        // Ÿ���� �ִٸ� �ϳ��� �����Բ�(�Ÿ������� üũ�ϸ� �߰��� �ٸ� ���͸� ����) >> �ش� ���Ͱ� ����� �� null�� �ٲپ� �ٸ� Ÿ���� �⵵��
        else
        {
            float distTmp = Vector3.Distance(transform.position, stateMachine.target.targetTransform.position);
            Vector3 dir = (stateMachine.target.targetTransform.position - transform.position).normalized;
            return new DetectInfo(stateMachine.target.targetTransform, distTmp, dir);
        }

        // Ž���� ���� ���ٸ� �Ʒ��� �����͸� ����Ʈ��
        return new DetectInfo(null, float.MaxValue, Vector3.zero);
    }
}
