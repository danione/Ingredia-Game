using UnityEngine;

public class AthameProjectile : SimpleProjectile
{
    private void Start()
    {
        SwapToMove();
    }

    public override void HandleCollision(Collider other)
    {
        IUnitStats unitStats = other.GetComponent<IUnitStats>();
        bool isValidTarget = target.Contains(other.tag);

        if (isValidTarget && unitStats != null)
        {
            unitStats.TakeDamage(strength);
            GameEventHandler.Instance.DestroyObject(gameObject);
        }
        else if (isValidTarget || (!other.GetComponent<SimpleProjectile>()?.IsSourcePlayer() ?? false))
        {
            GameEventHandler.Instance.DestroyObject(gameObject);
            GameEventHandler.Instance.DestroyObject(other.gameObject);
        }
    }
}
