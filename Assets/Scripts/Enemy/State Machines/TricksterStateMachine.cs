using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterStateMachine : EntityStateMachine
{
    public TricksterGatheringState TricksterGatheringState;
    public IdleState IdleState;

    public TricksterStateMachine(List<GameObject> ingredientsCaptured, float maxTimeGathering,  int maxIngredientsNeeded, Transform rotator)
    {
        TricksterGatheringState = new TricksterGatheringState(ingredientsCaptured, maxTimeGathering, maxIngredientsNeeded, rotator);
        IdleState = new IdleState();
    }
}
