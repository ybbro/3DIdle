public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.animData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.animData.RunParameterHash);
    }

    public override void Update()
    {
        // Ÿ�� �������� �̵�/ȸ��
        MoveAuto();
    }
}
