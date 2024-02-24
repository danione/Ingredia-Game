using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SteamballProjectile : SimpleProjectile
{
    [SerializeField] private float areaOfEffect = 8f;
    private static bool isAffectingPlayer = true;

    public override void HandleCollision(Collider other)
    {
        Collider[] nearbyEnemies = Physics.OverlapSphere(gameObject.transform.position, areaOfEffect);
        Collider[] activeObjects = nearbyEnemies.Where(collider => collider.gameObject.activeSelf).ToArray();

        foreach (Collider c in activeObjects)
        {
            IUnitStats stats = c.GetComponent<IUnitStats>();

            if ((!isAffectingPlayer && c.CompareTag("Player")) || !IsATarget(c.tag)) continue;

            if (stats != null)
            {
                stats.TakeDamage(strength);
            }
            else
            {
                GameEventHandler.Instance.DestroyObject(c.gameObject);
            }
        }
        GameEventHandler.Instance.DestroyObject(gameObject);
    }

    private bool IsATarget(string tag)
    {
        return target.Contains(tag);
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
