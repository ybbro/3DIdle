public abstract class StateMachine
{
    protected IState currentState;

    // 상태 변화
    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    // 입력에 대한 처리
    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    // 상태 업데이트
    public void Update()
    {
        currentState?.Update();
    }

    // 물리 상태 업데이트
    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}
