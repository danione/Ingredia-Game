using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStopperEnemy : Enemy
{
    [SerializeField] private int maxStopperPoints;
    [SerializeField] private float timeInStopperState;

    private TimeStopperStateMachine _stateMachine;

    private void Start()
    {
        _stateMachine = new TimeStopperStateMachine(maxStopperPoints, enemyData.spawnBoundaries, timeInStopperState);
        _stateMachine.Initialise(_stateMachine.TimeStopState);
        GameEventHandler.Instance.FinishedTimeStopState += OnFinishedTimeStopState;
    }

    private void OnFinishedTimeStopState()
    {
        GameEventHandler.Instance.ReleaseAllTimeStopPoints(gameObject);
        _stateMachine.TransitiontTo(_stateMachine.IdleState);
    }

    private void Update()
    {
        _stateMachine.CurrentState.Update();
    }
    
}
