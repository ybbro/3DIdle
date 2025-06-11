using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }

    public virtual void Enter()
    {
        //AddActionsCallbacks();
    }

    public virtual void Exit()
    {
        //RemoveActionsCallbacks();
    }

    protected virtual void AddActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        input.playerActions.Run.canceled += OnRunCanceled;
        input.playerActions.Run.started += OnRunStarted;
    }

    protected virtual void RemoveActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        input.playerActions.Run.canceled -= OnRunCanceled;
        input.playerActions.Run.started -= OnRunStarted;
    }

    public virtual void HandleInput()
    {
       // ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        
    }

    protected virtual void OnRunCanceled(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }

    // 애니메이션 재생/멈춤
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.animator.SetBool(animationHash, false);
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.playerActions.Run.ReadValue<Vector2>();
    }

    protected void MoveAuto()
    {
        Move(stateMachine.target.direction);
        Rotate(stateMachine.target.direction);
    }

    private void MoveByInput()
    {
        Vector3 movementDirection = GetMovementDirection().normalized;

        Move(movementDirection);

        Rotate(movementDirection);
    }

    // 입력 방향을 받아오도록
    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();

        stateMachine.Player.Controller.Move(
            (direction * movementSpeed) * Time.deltaTime
        );
    }

    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.MovementSpeed;// * stateMachine.MovementSpeedModifier;
        return moveSpeed;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            playerTransform.rotation = Quaternion.LookRotation(direction);
        }
    }
}