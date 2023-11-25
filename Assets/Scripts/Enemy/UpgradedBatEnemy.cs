using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradedBatEnemy : BatEnemy
{
    protected override void Shoot()
    {
        base.Shoot();
    }

    protected override void DestroyEnemy()
    {
        return;
    }
}
