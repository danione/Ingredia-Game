using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRitualistStateMachine : EntityStateMachine
{
    public BatMoveState MoveState;
    public IdleState IdleState;
    public BatEnhancedMovementState FusionAttackState;
    public RevultedState RevultedState;

    public BatRitualistStateMachine(PlayerController controller, Enemy currentUnit, EnemyStateData stateData)
    {
        this.MoveState = new BatMoveState(controller, currentUnit, stateData);
        this.IdleState = new IdleState();
        this.FusionAttackState = new BatEnhancedMovementState(currentUnit);
        this.RevultedState = new RevultedState(currentUnit, stateData);
    }
}
