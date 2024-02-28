using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStopperEnemy : Enemy
{
    [SerializeField] private int maxStopperPoints;
    [SerializeField] private float timeInStopperState;
    [SerializeField] private float idleCooldown;

    private TimeStopperStateMachine _stateMachine;

    public override void ResetEnemy()
    {
        base.ResetEnemy();

        if(_stateMachine == null)
        {
            _stateMachine = new TimeStopperStateMachine(maxStopperPoints, enemyData.spawnBoundaries, timeInStopperState, gameObject);
            StartCoroutine(WaitForInit());
        }
        else _stateMachine.TransitiontTo(_stateMachine.TimeStopState);
        GameEventHandler.Instance.FinishedTimeStopState += OnFinishedTimeStopState;
    }

    private IEnumerator WaitForInit()
    {
        yield return new WaitForEndOfFrame();
        _stateMachine.Initialise(_stateMachine.TimeStopState);

    }

    private void OnFinishedTimeStopState()
    {
        _stateMachine.TransitiontTo(_stateMachine.IdleState);
        GameEventHandler.Instance.ReleaseAllTimeStopPoints(gameObject);
        StartCoroutine(TransformBackToTimeStopState());
    }

    public override void DestroyEnemy()
    {
        GameEventHandler.Instance.ReleaseAllTimeStopPoints(gameObject);
        try
        {
            GameEventHandler.Instance.FinishedTimeStopState -= OnFinishedTimeStopState;

        }
        catch { }

        StopAllCoroutines();
        base.DestroyEnemy();
    }

    private IEnumerator TransformBackToTimeStopState()
    {
        yield return new WaitForSeconds(idleCooldown);
        _stateMachine.Initialise(_stateMachine.TimeStopState);
    }

    private void Update()
    {
        _stateMachine.CurrentState?.Update();
    }
    
}
