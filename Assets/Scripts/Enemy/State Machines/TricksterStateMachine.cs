using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterStateMachine : EntityStateMachine
{
    public TricksterGatheringState TricksterGatheringState;
    public IdleState IdleState;
    public TricksterThrowingState TricksterThrowingState;

    public TricksterStateMachine(List<GameObject> ingredientsCaptured, float maxTimeGathering,  int maxIngredientsNeeded, Transform rotator, TricksterEnemy trickster)
    {
        TricksterGatheringState = new TricksterGatheringState(ingredientsCaptured, maxTimeGathering, maxIngredientsNeeded, rotator);
        TricksterThrowingState = new TricksterThrowingState(ingredientsCaptured, trickster);
        IdleState = new IdleState();
    }
}
