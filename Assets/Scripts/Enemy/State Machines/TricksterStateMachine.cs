using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterStateMachine : EntityStateMachine
{
    public TricksterGatheringState TricksterGatheringState;
    public IdleState IdleState;
    public TricksterThrowingState TricksterThrowingState;
    public TricksterMoveState TricksterMoveState;

    public TricksterStateMachine(List<GameObject> ingredientsCaptured, float maxTimeGathering,  int maxIngredientsNeeded, Transform rotator, TricksterEnemy trickster, EnemyData enemyData)
    {
        TricksterGatheringState = new TricksterGatheringState(ingredientsCaptured, maxTimeGathering, maxIngredientsNeeded, rotator);
        TricksterMoveState = new TricksterMoveState(enemyData, trickster);
        TricksterThrowingState = new TricksterThrowingState(ingredientsCaptured, trickster, TricksterMoveState);
        IdleState = new IdleState();
    }
}
