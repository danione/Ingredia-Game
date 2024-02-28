using UnityEngine;

public abstract class Enemy : MonoBehaviour, IUnitStats
{

    [SerializeField] protected EnemyData enemyData;
    public float Health => currentHealth;
    private float currentHealth;

    private void Awake()
    {
        if(enemyData != null)
            currentHealth = enemyData.health;
        GetComponent<Resettable>()?.SetResetFunction(ResetEnemy);
    }

    public virtual void Die()
    {
        GameEventHandler.Instance.DestroyEnemy(gameObject.transform.position);
        GameEventHandler.Instance.SpawnsGoldenNuggets(enemyData.goldDrop, gameObject.transform.position);
        GameEventHandler.Instance.DestroyObject(gameObject);
    }

    public BoundariesData Boundaries => enemyData.spawnBoundaries;

    public virtual void Heal(float amount)
    {
        currentHealth += amount;
    }

    public virtual void TakeDamage(float amount)
    {
        GameEventHandler.Instance.TakeDamage(transform.position, amount);
        currentHealth -= amount;
        if(currentHealth <= 0) { DestroyEnemy(); }
    }

    public virtual void DestroyEnemy()
    {
        Die();
    }

    public virtual void ResetEnemy()
    {
        currentHealth = enemyData.health;
    }
}
