using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BatEnemy : Enemy
{
    private BatRitualistStateMachine _state;

    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float boundaryMovement = 2f;
    private float currentPositionDifferenceX;

    private bool attacked = false;
    private static bool hasCollided = false;
    protected bool isUpgraded = false;
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
            Shoot();
        }
        else if(currentPositionDifferenceX >= boundaryMovement)
        {
            _state.TransitiontTo(_state.MoveState);
        }
    }

    protected virtual void Shoot()
    {
        Instantiate(projectile, gameObject.transform.position, projectile.rotation);
        StartCoroutine(Cooldown());
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

    private void OnTriggerEnter(Collider other)
    {
        if(!hasCollided && other.CompareTag("Enemy") && other.GetComponent<BatEnemy>() != null && !isUpgraded)
        {
            hasCollided = true;
            GameEventHandler.Instance.FuseTwoBats(gameObject.transform.position);
            Destroy(other.gameObject);
            Die();
        }
    }

    protected override void DestroyEnemy()
    {
        return;
    }

    public void Upgrade()
    {
        isUpgraded = true;
    }
}
