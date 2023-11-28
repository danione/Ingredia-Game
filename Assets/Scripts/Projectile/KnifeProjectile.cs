using UnityEngine;

public class KnifeProjectile : SimpleProjectile
{
    public override void HandleCollision(Collider other)
    {
        IUnitStats unitStats = other.GetComponent<IUnitStats>();
        bool isValidTarget = target.Contains(other.tag);

        if (isValidTarget && unitStats != null)
        {
            unitStats.TakeDamage(strength);
            Destroy(gameObject);
        }
        else if (isValidTarget)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
