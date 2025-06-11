public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        // 이동하지 않고
        //stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        // Idle 애니메이션
        StartAnimation(stateMachine.Player.animData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        // Idle 애니메이션 종료
        StopAnimation(stateMachine.Player.animData.IdleParameterHash);
    }

    //public override void Update()
    //{
    //    base.Update();
    //    if(stateMachine.MovementInput != UnityEngine.Vector2.zero)
    //    {
    //        stateMachine.ChangeState(stateMachine.RunState);
    //        return;
    //    }
    //}
}