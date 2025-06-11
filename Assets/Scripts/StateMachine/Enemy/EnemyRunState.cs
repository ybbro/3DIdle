public class EnemyRunState : EnemyBaseState
{
    public EnemyRunState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        // �޸��� �ִϸ��̼� ����
        StartAnimation(stateMachine.Enemy.animData.RunParameterHash);
    }

    public override void Exit()
    {
        // �޸��� �ִϸ��̼� ����
        StopAnimation(stateMachine.Enemy.animData.RunParameterHash);
    }

    public override void Update()
    {
        // �÷��̾� �������� �̵�/ȸ��
        Move();
    }
}
