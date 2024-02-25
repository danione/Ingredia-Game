using System.Collections.Generic;
using UnityEngine;

public class TricksterGatheringState : IState
{
    private float timeInState = 0f;
    private float timeMaxInState;
    private List<GameObject> controlledIngredients = new();
    private int maxIngredients;
    private Transform rotator;
    private bool isComplete = false;

    public TricksterGatheringState(List<GameObject> ingredientArray, float timeMaxInState, int maxIngredients, Transform rotator)
    {
        controlledIngredients = ingredientArray;
        this.timeMaxInState = timeMaxInState;
        this.maxIngredients = maxIngredients;
        this.rotator = rotator;
    }

    public void Enter()
    {
        timeInState = 0;
        rotator.gameObject.SetActive(true);
        isComplete = false;
    }

    private void Finished()
    {
        isComplete = true;
        rotator.gameObject.SetActive(false);
        GameEventHandler.Instance.CaptureNeededIngredients(rotator.transform.parent.GetComponent<TricksterEnemy>());
    }

    public void Exit()
    {

    }

    void IState.Update()
    {
        if(isComplete) return;

        if(maxIngredients < controlledIngredients.Count || timeInState > timeMaxInState)
        {
            Finished();
            return;
        }
        
        timeInState += Time.deltaTime;
    }
}
