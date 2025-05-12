using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinState_Idle : EnemyState
{
    private Enemy_Goblin enemy;
    public GoblinState_Idle(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Goblin _enemy) : base(_enemy, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <0) 
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
