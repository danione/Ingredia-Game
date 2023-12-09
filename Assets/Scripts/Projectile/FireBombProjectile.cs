using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBombProjectile : SimpleProjectile
{
    [SerializeField] private float areaOfEffect = 4f;
    private static bool isAffectingPlayer = true;

    public override void HandleCollision(Collider other)
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(gameObject.transform.position, areaOfEffect);
        foreach (Collider c in nearbyEnemies)
        {
            IUnitStats stats = c.GetComponent<IUnitStats>();

            if (!isAffectingPlayer && c.CompareTag("Player")) continue;

            if (stats != null)
            {
                stats.TakeDamage(strength);
            }
            else
            {
                GameEventHandler.Instance.DestroyObject(other.gameObject);
            }
        }
        GameEventHandler.Instance.DestroyObject(gameObject);
    }

    public void SetAreaOfEffect(float newArea)
    {
        areaOfEffect = newArea > 0 ? newArea : areaOfEffect;
    }

    public void StopAffectingPlayer()
    {
        isAffectingPlayer = false;
    }
}
