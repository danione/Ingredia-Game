using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopperStateMachine : EntityStateMachine
{
    public IdleState IdleState;
    public TimeStopState TimeStopState;

    public TimeStopperStateMachine(int maxPoints, BoundariesData data, float timeInStopState)
    {
        TimeStopState = new TimeStopState(maxPoints, data, timeInStopState);
        IdleState = new IdleState();
    }
}
