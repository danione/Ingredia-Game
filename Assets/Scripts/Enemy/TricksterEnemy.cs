using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterEnemy : Enemy
{
    [SerializeField] private float maxTimeGathering;
    [SerializeField] private int maxIngredientsNeeded;

    private TricksterStateMachine m_StateMachine;
    private List<GameObject> capturedIngredients;

    private void Start()
    {
        m_StateMachine = new TricksterStateMachine(capturedIngredients, maxTimeGathering, maxIngredientsNeeded);
        m_StateMachine.Initialise(m_StateMachine.TricksterGatheringState);
    }

    private void Update()
    {
        m_StateMachine.CurrentState.Update();
    }

    // Saves references to the currently circulating ingredients
    public void AddCapturedIngredient(GameObject ingredient)
    {
        capturedIngredients.Add(ingredient);
    }

    private void ChangeState()
    {

    }
}
