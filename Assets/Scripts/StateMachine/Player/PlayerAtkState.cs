public class PlayerAtkState : PlayerBaseState
{
    public PlayerAtkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.animData.AttackParameterHash);
        Deal();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.animData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
        // 공격 애니메이션이 끝나면, 공격 종료
        if (stateMachine.Player.animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateMachine.Player.animData.AttackParameterHash)
        {
            Exit();
        }
    }

    public void Deal()
    {
        if (stateMachine.target.targetTransform.GetComponent<Enemy>().GetDamage(stateMachine.Player.Data.BattleData.Atk))
        {
            stateMachine.target.targetTransform = null;
        }
    }
}
