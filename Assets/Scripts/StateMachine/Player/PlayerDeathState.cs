public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        // 죽음 애니메이션 시작
        stateMachine.Player.animator.SetTrigger(stateMachine.Player.animData.DeathParameterHash);
    }
}
