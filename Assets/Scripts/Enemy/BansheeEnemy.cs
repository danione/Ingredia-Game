using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BansheeEnemy : Enemy
{
    private bool hasReachedDestination = false;
    private bool hasDetectedPlayer = false;
    private bool isColliding = true;

    public BansheeStateMachine _state;
    [SerializeField] private Boundaries fieldOfMovement;
    [SerializeField] private float cooldownWaitingSeconds;
    [SerializeField] private float cooldownDetectionOfPlayer;
    private void Start()
    {
        _state = new BansheeStateMachine(this, fieldOfMovement);
        _state.Initialise(_state.MoveState);
        GameEventHandler.Instance.BansheeDetectedPlayer += OnPlayerInDetectionZone;
    }

    private void Update()
    {
        if (hasReachedDestination && !hasDetectedPlayer)
        {
            hasReachedDestination = false;
            StartCoroutine(CooldownOfReachedDestination(_state.MoveState));
        }
       _state.Update();
    }

    public void ReachedDestination()
    {
        hasReachedDestination = true;
    }

    IEnumerator CooldownOfReachedDestination(IState transitionTo)
    {
        _state.TransitiontTo(_state.IdleState);
        yield return new WaitForSeconds(cooldownWaitingSeconds);
        _state.TransitiontTo(transitionTo);

        if(transitionTo == _state.AttackState)
        {
            yield return new WaitForSeconds(cooldownWaitingSeconds);
            _state.TransitiontTo(_state.MoveState);
            StartCoroutine(CooldownOfDetection());
            hasDetectedPlayer = false;
        }
    }

    IEnumerator CooldownOfDetection()
    {
        yield return new WaitForSeconds(cooldownDetectionOfPlayer);
        SetTheCollider(true);
    }

    private void SetTheCollider(bool isEnabled)
    {
        isColliding = isEnabled;
    }

    private void OnPlayerInDetectionZone()
    {
        if(isColliding)
        {
            hasReachedDestination = false;
            hasDetectedPlayer = true;
            StartCoroutine(CooldownOfReachedDestination(_state.AttackState));
            SetTheCollider(false);
        }
    }

    protected override void DestroyEnemy()
    {
        InputEventHandler.instance.SetMovement(isMoving: true);
    }
}
