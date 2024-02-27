using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TimeStopState : IState
{
    private int maxAmountOfTimeStopPoints;
    private BoundariesData data;
    private float timeInState = 0f;
    private float defaultTimeInState;
    private bool hasFinished = false;

    public TimeStopState(int maxAmount, BoundariesData data, float timeInState)
    {
        maxAmountOfTimeStopPoints = maxAmount;
        this.data = data;
        this.defaultTimeInState = timeInState;
    }
    
    public void Enter()
    {
        hasFinished = false;
        timeInState = 0f;
        for(int i = 0; i < maxAmountOfTimeStopPoints; i++)
        {
            Vector3 point = new Vector3(Random.Range(data.xLeftMax, data.xRightMax),
                Random.Range(data.yBottomMax, data.yTopMax), 2);
            GameEventHandler.Instance.SpawnTimeStopPoint(point);
        }

    }

    public void Exit()
    {

    }

    private void Finish()
    {
        hasFinished = true;
        GameEventHandler.Instance.FinishedTimeStopState();
    }

    void IState.Update()
    {
        if (hasFinished) return;

        if(timeInState > defaultTimeInState)
        {
            Finish();
        }
        timeInState += Time.deltaTime;
    }
}
