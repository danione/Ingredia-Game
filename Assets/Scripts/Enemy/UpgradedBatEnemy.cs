using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UpgradedBatEnemy : BatEnemy
{
    [SerializeField] private float rapidShootRateCooldown = 2f;
    protected override void Shoot()
    {
        if(_state.CurrentState == _state.FusionAttackState)
        {
            Instantiate(projectile, gameObject.transform.position, projectile.rotation);
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
        return;
    }
}
