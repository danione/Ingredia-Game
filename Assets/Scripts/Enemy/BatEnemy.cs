using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BatEnemy : Enemy
{
    protected BatRitualistStateMachine _state;

    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float boundaryMovement = 2f;
    private float currentPositionDifferenceX;

    protected bool attacked = false;
    private static bool hasCollided = false;
    protected bool isUpgraded = false;
    [SerializeField] protected ObjectsSpawner spawner;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float upgradedStateDuration;
    

    private void Start()
    {
        _state = new BatRitualistStateMachine(PlayerController.Instance, this, movementSpeed);
        _state.Initialise(_state.MoveState);
        currentPositionDifferenceX = GetCurrentPositionDifferenceX();
    }

    public override void ResetEnemy()
    {
        if(_state != null)
            _state.Initialise(_state.MoveState);
        attacked = false;
        hasCollided = false;
    }

    private void Update()
    {
        _state.Update();

        if (_state.CurrentState == _state.FusionAttackState)
        {
            if (!attacked)
            {
                attacked = true;
                Shoot();
            }
        }
        else if(_state.CurrentState == _state.MoveState || _state.CurrentState == _state.IdleState)
        {
            FollowPlayerAndShoot();
        }
    }

    private void FollowPlayerAndShoot()
    {
        currentPositionDifferenceX = GetCurrentPositionDifferenceX();
        if (currentPositionDifferenceX < boundaryMovement && !attacked)
        {
            attacked = true;
            _state.TransitiontTo(_state.IdleState);
            Shoot();
        }
        else if (currentPositionDifferenceX >= boundaryMovement)
        {
            _state.TransitiontTo(_state.MoveState);
        }
    }

    protected virtual void Shoot()
    {
        var creature = spawner._pool.Get();
        if (creature == null) return;

        creature.transform.position = gameObject.transform.position;
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
        if(!hasCollided && other.CompareTag("Enemy") && !isUpgraded)
        {
            Fusion(other);
        }
    }

    protected virtual void Fusion(Collider other)
    {
        BatEnemy batEnemy = other.GetComponent<BatEnemy>();
        if (batEnemy == null) return;
        hasCollided = true;

        if(isUpgraded && !batEnemy.isUpgraded)
        {
            Destroy(other.gameObject);
        }
        else if (!isUpgraded)
        {
            if (batEnemy.isUpgraded)
            {
                batEnemy.FusingState();
            }
            else
            {
                GameEventHandler.Instance.FuseTwoBats(gameObject.transform.position);
                Destroy(other.gameObject);
            }
            Die();
        }
    }

    protected override void DestroyEnemy()
    {
        base.DestroyEnemy();
        hasCollided = false;
    }

    public void Upgrade()
    {
        isUpgraded = true;
    }

    public void FusingState()
    {
        if(_state.CurrentState != _state.FusionAttackState)
        {
            _state.TransitiontTo(_state.FusionAttackState);
            StartCoroutine(TransitionBackToNormalState());
        }
    }

    IEnumerator TransitionBackToNormalState()
    {
        yield return new WaitForSeconds(upgradedStateDuration);
        _state.TransitiontTo(_state.MoveState);
        attacked = false;
    }
}
