using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRitualistStateMachine : EntityStateMachine
{
    public BatMoveState MoveState;
    public IdleState IdleState;

    public BatRitualistStateMachine(PlayerController controller, Enemy currentUnit, float movementSpeed)
    {
        this.MoveState = new BatMoveState(controller, currentUnit, movementSpeed);
        this.IdleState = new IdleState();
    }
}
