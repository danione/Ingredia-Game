using UnityEngine;

public class BansheeStateMachine : EntityStateMachine
{
    public IdleState IdleState;
    public PickDirectionState PickDirectionState;

    public BansheeStateMachine(Transform thisGameObject)
    {
        IdleState = new IdleState();
        PickDirectionState = new PickDirectionState(thisGameObject);
    }

}
