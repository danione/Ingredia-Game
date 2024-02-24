using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterStateMachine : EntityStateMachine
{
    public TricksterGatheringState TricksterGatheringState;
    public IdleState IdleState;

    public TricksterStateMachine(Transform currentObject)
    {
        TricksterGatheringState = new TricksterGatheringState(currentObject);
        IdleState = new IdleState();
    }
}
