public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        // �̵����� �ʰ�
        //stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        // Idle �ִϸ��̼�
        StartAnimation(stateMachine.Player.animData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        // Idle �ִϸ��̼� ����
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