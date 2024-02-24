using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterProjectile : SimpleProjectile
{
    public override void HandleCollision(Collider other)
    {
        if (target.Contains(other.tag))
        {
            IUnitStats stats = other.GetComponent<IUnitStats>();
            stats?.TakeDamage(strength);
            GameEventHandler.Instance.DestroyObject(gameObject);
        }
    }
}
