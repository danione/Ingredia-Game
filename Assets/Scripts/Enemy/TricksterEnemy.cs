using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterEnemy : Enemy
{
    private TricksterStateMachine m_StateMachine;

    private void Start()
    {
        m_StateMachine = new TricksterStateMachine(this.transform);
        m_StateMachine.Initialise(m_StateMachine.TricksterGatheringState);
    }

    private void Update()
    {
        m_StateMachine.CurrentState.Update();
    }
}
