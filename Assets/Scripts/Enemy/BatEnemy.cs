using System;
using System.Collections;
using UnityEngine;

public class BatEnemy : Enemy
{
    protected BatRitualistStateMachine _state;
    protected bool attacked = false;
    [NonSerialized]public bool hasCollided = false;
    private static BatFuser fuser;
    [SerializeField] public bool isUpgraded = false;
    [SerializeField] protected ObjectsSpawner spawner;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float upgradedStateDuration;
    [SerializeField] private GameObject SpawnManager;
    [SerializeField] private EnemyStateData stateData;

    private void Start()
    {
        if (fuser == null) fuser = SpawnManager.GetComponent<BatFuser>();
        _state = new BatRitualistStateMachine(PlayerController.Instance, this, stateData);
        _state.Initialise(_state.MoveState);
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        if(_state == null)
        {
            _state = new BatRitualistStateMachine(PlayerController.Instance, this, stateData);
            _state.Initialise(_state.MoveState);
        }
        else
            _state.TransitiontTo(_state.MoveState);
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
    }

    public virtual void Shoot()
    {
        if (attacked) return;
        attacked = true;
        var creature = spawner._pool.Get();
        if (creature == null) return;

        creature.transform.position = gameObject.transform.position;
        creature.GetComponent<AthameProjectile>()?.SwapToMove();
        StartCoroutine(Cooldown());
    }


    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(spawnCooldown);
        attacked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!hasCollided && !isUpgraded && other.CompareTag("Enemy"))
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
