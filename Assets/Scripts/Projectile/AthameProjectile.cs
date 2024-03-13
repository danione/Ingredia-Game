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
        else if (isValidTarget)
        {
            bool isOtherFromPlayer = other.gameObject.GetComponent<SimpleProjectile>()?.IsSourcePlayer() ?? false;
            if((isOtherFromPlayer && !IsSourcePlayer()) || (!isOtherFromPlayer && IsSourcePlayer()) || (!isOtherFromPlayer && !IsSourcePlayer()))
            {
                GameEventHandler.Instance.DestroyObject(gameObject);
                GameEventHandler.Instance.DestroyObject(other.gameObject);
            }
        }
    }
}
