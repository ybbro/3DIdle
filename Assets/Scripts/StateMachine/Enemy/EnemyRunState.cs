public class EnemyRunState : EnemyBaseState
{
    public EnemyRunState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        // 달리기 애니메이션 시작
        StartAnimation(stateMachine.Enemy.animData.RunParameterHash);
    }

    public override void Exit()
    {
        // 달리기 애니메이션 멈춤
        StopAnimation(stateMachine.Enemy.animData.RunParameterHash);
    }

    public override void Update()
    {
        // 플레이어 방향으로 이동/회전
        Move();
    }
}
