using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBombProjectile : SimpleProjectile
{
    [SerializeField] private float areaOfEffect = 4f;

    public override void HandleCollision(Collider other)
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(gameObject.transform.position, areaOfEffect);
        foreach (Collider c in nearbyEnemies)
        {
            IUnitStats stats = c.GetComponent<IUnitStats>();
            if(stats != null)
            {
                stats.TakeDamage(strength);
            }
            else
            {
                Destroy(c.gameObject);
            }
        }
        Destroy(gameObject);
    }

    public void SetAreaOfEffect(float newArea)
    {
        areaOfEffect = newArea > 0 ? newArea : areaOfEffect;
    }
}
