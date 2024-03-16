using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SteamballProjectile : SimpleProjectile
{
    [SerializeField] private float areaOfEffect = 8f;
    private static bool isAffectingPlayer = true;

    private void Start()
    {
        SwapToMove();
    }

    public override void HandleCollision(Collider other)
    {
        bool validCollision = IsATarget(other.tag);
        Collider[] nearbyEnemies = Physics.OverlapSphere(gameObject.transform.position, areaOfEffect);
        Collider[] activeObjects = nearbyEnemies.Where(collider => collider.gameObject.activeSelf).ToArray();

        if (validCollision)
        {
            foreach (Collider c in activeObjects)
            {
                if ((!isAffectingPlayer && c.CompareTag("Player")) || !IsATarget(c.tag)) continue;

                IUnitStats stats = c.GetComponent<IUnitStats>();

                if (stats != null)
                {
                    stats.TakeDamage(strength);
                }
                else if (c.CompareTag("Dangerous Object") || c.CompareTag("Projectile") || c.CompareTag("Ingredient"))
                {
                    GameEventHandler.Instance.DestroyObject(c.gameObject);
                }
            }
            GameEventHandler.Instance.DestroyObject(gameObject);
        }
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

    public void ResetAffectingPlayer()
    {
        isAffectingPlayer = true;
    }
}
