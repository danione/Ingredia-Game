using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState : MonoBehaviour
{
    public IState CurrentState { get; private set; }

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
