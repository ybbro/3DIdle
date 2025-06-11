public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        // ���� �ִϸ��̼� ����
        stateMachine.Player.animator.SetTrigger(stateMachine.Player.animData.DeathParameterHash);
    }
}
