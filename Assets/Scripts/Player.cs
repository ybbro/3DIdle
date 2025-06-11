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

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        // 현재 체력을 최대 체력으로
        HPCurrent = stateMachine.Player.Data.BattleData.HPMax;
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        // 죽었다면 더 연산하지 않도록
        if (isDead)
            return;

        stateMachine.Update();

        // 타겟이 공격 범위 안에 있다면
        if (stateMachine.target.distance < stateMachine.Player.Data.GroundData.AtkableRange)
        {
            // 카운트를 세어주다
            atkDelayCount += Time.deltaTime;
            // 공격 딜레이가 지났다면
            if (atkDelayCount >= stateMachine.Player.Data.BattleData.AtkDelay)
            {
                atkDelayCount = 0;
                // 공격 모션 재생
                stateMachine.ChangeState(stateMachine.AtkState);
                return;
            }
            // 공격 딜레이 동안 대기 모션
            // 트랜지션 도중이 아니고, 대기 모션이 아니라면
            else if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateMachine.Player.animData.IdleParameterHash)
            {
                // 대기
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
        // 타겟이 공격 범위 ~ 탐색 범위 안에 있다면
        else if (stateMachine.target.distance <= stateMachine.Player.Data.GroundData.DetectRange)
        {
            stateMachine.ChangeState(stateMachine.RunState);
            return;
        }
        // 플레이어가 탐색 범위 내에 없다면, idle로 변경
        // 트랜지션 도중이 아니고, 대기 모션이 아니라면
        else if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateMachine.Player.animData.IdleParameterHash)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        // 인풋은 잠시 죽여놓기로 >> 시간이 만약 남는다면 자동 풀고 손컨도 있게끔
        // stateMachine.HandleInput();
    }

    private void FixedUpdate()
    {
        stateMachine.target = DetectTarget();
        stateMachine.PhysicsUpdate();
    }

    // 대미지 받음 및 사망 처리
    public void GetDamage(float damage)
    {
        HPCurrent -= damage;
        if (HPCurrent <= 0)
        {
            HPCurrent = 0;
            isDead = true;
            stateMachine.ChangeState(stateMachine.DeathState);
        }
        // 체력바 변화량 표시
        HPBar.ChangeHPBar(HPCurrent, stateMachine.Player.Data.BattleData.HPMax);
    }

    public void Heal(float HealAmount)
    {
        float hpMax = stateMachine.Player.Data.BattleData.HPMax;
        HPCurrent = Mathf.Min(HPCurrent + HealAmount, hpMax);
        // 체력바 변화량 표시
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
        }
        // 타겟이 있다면 하나만 때리게끔(거리만으로 체크하면 중간에 다른 몬스터를 때림) >> 해당 몬스터가 사망할 때 null로 바꾸어 다른 타겟을 잡도록
        else
        {
            float distTmp = Vector3.Distance(transform.position, stateMachine.target.targetTransform.position);
            Vector3 dir = (stateMachine.target.targetTransform.position - transform.position).normalized;
            return new DetectInfo(stateMachine.target.targetTransform, distTmp, dir);
        }

        // 탐색한 적이 없다면 아래의 데이터를 디폴트로
        return new DetectInfo(null, float.MaxValue, Vector3.zero);
    }
}
