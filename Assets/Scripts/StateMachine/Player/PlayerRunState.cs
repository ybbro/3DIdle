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
        // 타겟 방향으로 이동/회전
        MoveAuto();
    }
}
