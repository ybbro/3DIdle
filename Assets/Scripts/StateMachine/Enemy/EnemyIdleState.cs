using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        // 대기 애니메이션 시작
        StartAnimation(stateMachine.Enemy.animData.IdleParameterHash);
    }

    public override void Exit()
    {
        // 대기 애니메이션 멈춤
        StopAnimation(stateMachine.Enemy.animData.IdleParameterHash);
    }

    public override void Update()
    {
        
    }
}
