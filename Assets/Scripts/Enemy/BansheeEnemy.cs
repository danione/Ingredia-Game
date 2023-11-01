using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BansheeEnemy : Enemy
{
    private bool hasReachedDestination = false;
    public BansheeStateMachine _state;
    [SerializeField] private Boundaries fieldOfMovement;
    [SerializeField] private float cooldownWaitingSeconds;
    private void Start()
    {
        _state = new BansheeStateMachine(this, fieldOfMovement);
        _state.Initialise(_state.MoveState);
    }

    private void Update()
    {
        if (hasReachedDestination)
        {
            hasReachedDestination = false;
            _state.TransitiontTo(_state.IdleState);
            StartCoroutine(CooldownOfMakingDecision());
        }
       _state.Update();
    }

    public void ReachedDestination()
    {
        hasReachedDestination = true;
    }

    IEnumerator CooldownOfMakingDecision()
    {
        yield return new WaitForSeconds(cooldownWaitingSeconds);
        _state.TransitiontTo(_state.MoveState);
    }

    IEnumerator DisableTheCollider()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(cooldownWaitingSeconds);
        gameObject.GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        _state.TransitiontTo(_state.IdleState);
        StartCoroutine(DisableTheCollider());
        _state.TransitiontTo(_state.MoveState);
    }
}
