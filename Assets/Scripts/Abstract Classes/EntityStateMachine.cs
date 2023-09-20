using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateMachine
{
    public IState CurrentState { get; private set; }

    public MoveState MoveState;
    public IdleState IdleState;

    public EntityStateMachine(PlayerController controller, Enemy currentUnit, float movementSpeed)
    {
        this.MoveState = new MoveState(controller, currentUnit, movementSpeed);
        this.IdleState = new IdleState();
    }

    public void Initialise(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitiontTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}
