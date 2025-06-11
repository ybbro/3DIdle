using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        // ��� �ִϸ��̼� ����
        StartAnimation(stateMachine.Enemy.animData.IdleParameterHash);
    }

    public override void Exit()
    {
        // ��� �ִϸ��̼� ����
        StopAnimation(stateMachine.Enemy.animData.IdleParameterHash);
    }

    public override void Update()
    {
        
    }
}
