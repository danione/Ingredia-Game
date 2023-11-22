using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BansheeEnemy : Enemy
{
    private bool hasReachedDestination = false;
    private bool hasDetectedPlayer = false;

    public BansheeStateMachine _state;
    [SerializeField] private Boundaries fieldOfMovement;
    [SerializeField] private float cooldownWaitingSeconds;
    [SerializeField] private float cooldownDetectionOfPlayer;
    private void Start()
    {
        _state = new BansheeStateMachine(this, fieldOfMovement);
        _state.Initialise(_state.MoveState);
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
        gameObject.GetComponent<Collider>().enabled = isEnabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        hasReachedDestination = false;
        hasDetectedPlayer = true;
        StartCoroutine(CooldownOfReachedDestination(_state.AttackState));
        SetTheCollider(false);
    }

    private void OnDestroy()
    {
        InputEventHandler.instance.SetMovement(isMoving: true);
    }
}
