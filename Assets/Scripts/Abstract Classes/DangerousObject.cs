using UnityEngine;

public class DangerousObject : FallableObject
{
    [SerializeField] private int healthCost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.TakeDamage(healthCost);
            }
            Destroy(gameObject);
        }
    }
}
