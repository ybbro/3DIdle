public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        // ��� �߰�
        GameManager.instance.wallet.GoldChange(stateMachine.Enemy.Data.DropData.DropGold);
        // ���� �ִϸ��̼� ����
        stateMachine.Enemy.animator.SetTrigger(stateMachine.Enemy.animData.DeathParameterHash);
    }
}
