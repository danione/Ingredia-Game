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
            GameEventHandler.Instance.DestroyObject(gameObject);
        }
        else if (isValidTarget)
        {

            GameEventHandler.Instance.DestroyObject(gameObject);
            GameEventHandler.Instance.DestroyObject(other.gameObject);
        }
    }
}
