using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        //stateMachine.MovementSpeedModifier = groundData.RunSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Player.animData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.animData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput.Equals(Vector2.zero)) return;
        stateMachine.ChangeState(stateMachine.IdleState);
        base.OnRunCanceled(context);
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        //stateMachine.ChangeState(stateMachine.RunState);
        base.OnRunStarted(context);
    }
}
