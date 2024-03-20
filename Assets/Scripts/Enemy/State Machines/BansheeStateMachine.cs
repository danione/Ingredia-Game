using UnityEngine;

public class BansheeStateMachine : EntityStateMachine
{
    public IdleState IdleState;
    public BansheeMoveState MoveState;
    public BansheeAttackState AttackState;

    public BansheeStateMachine(BansheeEnemy thisGameObject, EnemyData data, EnemyStateData stateData)
    {
        IdleState = new IdleState();
        MoveState = new BansheeMoveState(PlayerController.Instance, thisGameObject, stateData, data.spawnBoundaries);
        AttackState = new BansheeAttackState(PlayerController.Instance, thisGameObject);
    }

}
