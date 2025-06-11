public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        // 골드 추가
        GameManager.instance.wallet.GoldChange(stateMachine.Enemy.Data.DropData.DropGold);
        // 죽음 애니메이션 시작
        stateMachine.Enemy.animator.SetTrigger(stateMachine.Enemy.animData.DeathParameterHash);
    }
}
