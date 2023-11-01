using UnityEngine;

public class BansheeStateMachine : EntityStateMachine
{
    public IdleState IdleState;
    public BansheeMoveState MoveState;
    public BansheeAttackState AttackState;

    public BansheeStateMachine(BansheeEnemy thisGameObject, Boundaries fieldOfMovement)
    {
        IdleState = new IdleState();
        MoveState = new BansheeMoveState(PlayerController.Instance, thisGameObject, 10.0f, fieldOfMovement);
        AttackState = new BansheeAttackState(PlayerController.Instance, thisGameObject);
    }

}
