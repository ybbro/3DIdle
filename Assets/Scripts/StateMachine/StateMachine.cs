public abstract class StateMachine
{
    protected IState currentState;

    // ���� ��ȭ
    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    // �Է¿� ���� ó��
    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    // ���� ������Ʈ
    public void Update()
    {
        currentState?.Update();
    }

    // ���� ���� ������Ʈ
    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}
