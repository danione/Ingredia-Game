using System.Collections.Generic;
using UnityEngine;

public class TricksterGatheringState : IState
{
    private float timeInState = 0f;
    private float timeMaxInState;
    private List<GameObject> controlledIngredients;
    private int maxIngredients;

    public TricksterGatheringState(List<GameObject> ingredientArray, float timeMaxInState, int maxIngredients)
    {
        controlledIngredients = ingredientArray;
        this.timeMaxInState = timeMaxInState;
        this.maxIngredients = maxIngredients;
    }

    private void Reset()
    {
        timeInState = 0;
    }

    public void Enter()
    {
        Reset();
    }

    public void Exit()
    {
    }

    void IState.Update()
    {
        if(maxIngredients >= controlledIngredients.Count || timeInState > timeMaxInState)
        {
            Exit();
            return;
        }

        timeInState += Time.deltaTime;
        
    }
}
