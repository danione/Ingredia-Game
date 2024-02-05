using System.Collections;
using UnityEngine;

public class UpgradedBatEnemy : BatEnemy
{
    [SerializeField] private float rapidShootRateCooldown = 2f;
    protected override void Shoot()
    {
        if(_state.CurrentState == _state.FusionAttackState)
        {
            spawner._pool.Get().transform.position = gameObject.transform.position;
            StartCoroutine(RapidShootCooldown());
        }
        else if(_state.CurrentState != _state.FusionAttackState)
        {
            base.Shoot();
        }
    }

    IEnumerator RapidShootCooldown()
    {
        yield return new WaitForSeconds(rapidShootRateCooldown);
        attacked = false;
    }

    protected override void DestroyEnemy()
    {
        base.DestroyEnemy();
        attacked = false;
    }
}
