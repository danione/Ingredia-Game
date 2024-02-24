using UnityEngine;

public class TricksterGatheringState : IState
{
    private float timeInState = 0f;
    private const float timeMaxInState = 3f;
    private Transform thisObject;

    public TricksterGatheringState(Transform currentObject)
    {
        thisObject = currentObject;
    }

    public void Enter()
    {
        timeInState = 0;
    }

    public void Exit()
    {
    }

    void IState.Update()
    {
       if(timeInState < timeMaxInState)
        {
            timeInState += Time.deltaTime;

        }
        else
        {
            Debug.Log("Elapsed");
        }
    }
}
