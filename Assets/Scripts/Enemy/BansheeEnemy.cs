using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BansheeEnemy : Enemy
{
    private bool hasReachedDestination = false;
    private bool hasDetectedPlayer = false;
    private bool isColliding = true;

    public BansheeStateMachine _state;


    [SerializeField] private BoundariesData fieldOfMovement;
    [SerializeField] private float cooldownWaitingSeconds;
    [SerializeField] private float cooldownDetectionOfPlayer;
    private void Start()
    {
        _state = new BansheeStateMachine(this, fieldOfMovement);
        ResetEnemy();
        GameEventHandler.Instance.BansheeDetectedPlayer += OnPlayerInDetectionZone;
    }

    public override void ResetEnemy()
    {
        _state.Initialise(_state.MoveState);
        hasDetectedPlayer = false;
        hasDetectedPlayer = false;
        isColliding = true;
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
        base.DestroyEnemy();
        InputEventHandler.instance.SetMovement(isMoving: true);
    }
}
