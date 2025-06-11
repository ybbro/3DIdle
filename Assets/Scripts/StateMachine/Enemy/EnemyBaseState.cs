using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
    }

    public virtual void Enter()
    {
       
    }

    public virtual void Exit()
    {
        
    }

    public virtual void HandleInput()
    {
        
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {

    }


    // �ִϸ��̼� ���/����
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.animator.SetBool(animationHash, false);
    }

    // �̵�/ȸ��
    protected void Move()
    {
        Move(stateMachine.target.direction);
        Rotate(stateMachine.target.direction);
    }

    // �ش� �������� �̵�
    private void Move(Vector3 direction)
    {
        stateMachine.Enemy.Controller.Move(
            (direction * stateMachine.MovementSpeed) * Time.deltaTime
        );
    }

    // ĳ���Ͱ� Ÿ�� ������ �ٶ󺸵���
    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform transformTmp = stateMachine.Enemy.transform;
            transformTmp.rotation = Quaternion.LookRotation(direction);
        }
    }
}