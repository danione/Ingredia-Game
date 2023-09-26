using System;
using System.Collections;
using UnityEngine;

public class BatEnemy : Enemy
{
    private BatRitualistStateMachine _state;

    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float boundaryMovement = 2f;
    private float currentPositionDifferenceX;

    private bool attacked = false;
    [SerializeField] private Transform projectile;
    [SerializeField] private float spawnCooldown;

    private void Start()
    {
        _state = new BatRitualistStateMachine(PlayerController.Instance, this, movementSpeed);
        _state.Initialise(_state.MoveState);
        currentPositionDifferenceX = GetCurrentPositionDifferenceX();
    }

    private void Update()
    {
        _state.Update();
        currentPositionDifferenceX = GetCurrentPositionDifferenceX();
        if (currentPositionDifferenceX < boundaryMovement && !attacked)
        {
            attacked = true;
            _state.TransitiontTo(_state.IdleState);
            Instantiate(projectile, gameObject.transform.position, projectile.rotation);
            StartCoroutine(Cooldown());
        } else if(currentPositionDifferenceX >= boundaryMovement)
        {
            _state.TransitiontTo(_state.MoveState);
        }
    }

    private float GetCurrentPositionDifferenceX()
    {
        return Mathf.Abs(PlayerController.Instance.gameObject.transform.position.x - transform.position.x);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(spawnCooldown);
        attacked = false;
    }
}
