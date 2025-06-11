public class EnemyAtkState : EnemyBaseState
{
    public EnemyAtkState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        // 공격 애니메이션 시작
        StartAnimation(stateMachine.Enemy.animData.AttackParameterHash);
        // 대미지는 애니메이션 이벤트로 처리하거나 여기서 처리하기
        Deal();
    }

    public override void Exit()
    {
        // 공격 애니메이션 멈춤
        StopAnimation(stateMachine.Enemy.animData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
        // 공격 애니메이션이 끝나면, 공격 종료
        if(stateMachine.Enemy.animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateMachine.Enemy.animData.AttackParameterHash)
        {
            Exit();
        }
    }

    // 좀 더 자연스럽게 하려면 애니메이션에 이벤트로 넣는 게 좋으나 시간이..
    public void Deal()
    {
        stateMachine.target.targetTransform.GetComponent<Player>().GetDamage(stateMachine.Enemy.Data.BattleData.Atk);
    }
}
