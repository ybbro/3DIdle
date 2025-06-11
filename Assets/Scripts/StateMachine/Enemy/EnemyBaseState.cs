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


    // 애니메이션 재생/멈춤
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Enemy.animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Enemy.animator.SetBool(animationHash, false);
    }

    // 이동/회전
    protected void Move()
    {
        Move(stateMachine.target.direction);
        Rotate(stateMachine.target.direction);
    }

    // 해당 방향으로 이동
    private void Move(Vector3 direction)
    {
        stateMachine.Enemy.Controller.Move(
            (direction * stateMachine.MovementSpeed) * Time.deltaTime
        );
    }

    // 캐릭터가 타겟 방향을 바라보도록
    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform transformTmp = stateMachine.Enemy.transform;
            transformTmp.rotation = Quaternion.LookRotation(direction);
        }
    }
}