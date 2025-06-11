public class EnemyAtkState : EnemyBaseState
{
    public EnemyAtkState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        // ���� �ִϸ��̼� ����
        StartAnimation(stateMachine.Enemy.animData.AttackParameterHash);
        // ������� �ִϸ��̼� �̺�Ʈ�� ó���ϰų� ���⼭ ó���ϱ�
        Deal();
    }

    public override void Exit()
    {
        // ���� �ִϸ��̼� ����
        StopAnimation(stateMachine.Enemy.animData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
        // ���� �ִϸ��̼��� ������, ���� ����
        if(stateMachine.Enemy.animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateMachine.Enemy.animData.AttackParameterHash)
        {
            Exit();
        }
    }

    // �� �� �ڿ������� �Ϸ��� �ִϸ��̼ǿ� �̺�Ʈ�� �ִ� �� ������ �ð���..
    public void Deal()
    {
        stateMachine.target.targetTransform.GetComponent<Player>().GetDamage(stateMachine.Enemy.Data.BattleData.Atk);
    }
}
