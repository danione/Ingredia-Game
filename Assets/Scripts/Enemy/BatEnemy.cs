using System;
using System.Collections;
using UnityEngine;

public class BatEnemy : Enemy
{
    protected BatRitualistStateMachine _state;

    private float currentPositionDifferenceX;

    protected bool attacked = false;
    [NonSerialized]public bool hasCollided = false;
    private static BatFuser fuser;
    protected bool isUpgraded = false;
    [SerializeField] protected ObjectsSpawner spawner;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float upgradedStateDuration;
    [SerializeField] private float boundaryMovement;
    [SerializeField] private GameObject SpawnManager;
    [SerializeField] private float trackingCooldown;


    private void Start()
    {
        if (fuser == null) fuser = SpawnManager.GetComponent<BatFuser>();
        _state = new BatRitualistStateMachine(PlayerController.Instance, this, enemyData.movementSpeed);
        _state.Initialise(_state.MoveState);
        currentPositionDifferenceX = GetCurrentPositionDifferenceX();
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        if(_state != null)
            _state.Initialise(_state.MoveState);
        attacked = false;
        hasCollided = false;
        isUpgraded = false;
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
        else if(_state.CurrentState == _state.MoveState)
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
            StartCoroutine(TrackingPlayerCooldown());
            Shoot();
        }
        else if (currentPositionDifferenceX >= boundaryMovement)
        {
            _state.TransitiontTo(_state.MoveState);
        }
    }

    private IEnumerator TrackingPlayerCooldown()
    {
        yield return new WaitForSeconds(trackingCooldown);
        _state.TransitiontTo(_state.MoveState);
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
            hasCollided = true;
            fuser.Fuse(this);
        }
    }

    public override void DestroyEnemy()
    {
        hasCollided = false;
        base.DestroyEnemy();
    }
}
