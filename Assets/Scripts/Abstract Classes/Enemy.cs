using UnityEngine;

public abstract class Enemy : MonoBehaviour, IUnitStats
{

    [SerializeField] protected EnemyData enemyData;
    public float Health => currentHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = enemyData.health;
    }

    public virtual void Die()
    {
        GameEventHandler.Instance.DestroyEnemy(gameObject.transform.position);
        GameEventHandler.Instance.SpawnsGoldenNuggets(enemyData.goldDrop, gameObject.transform.position);
        GameEventHandler.Instance.DestroyObject(gameObject);
    }

    public BoundariesData Boundaries => enemyData.spawnBoundaries;

    public virtual Vector3 GetRandomPosition()
    {
        float xRandomPos = Random.Range(enemyData.spawnBoundaries.xLeftMax, enemyData.spawnBoundaries.xRightMax);
        float yRandomPos = Random.Range(enemyData.spawnBoundaries.yTopMax, enemyData.spawnBoundaries.yBottomMax);
        Vector3 randomPos = new Vector3(xRandomPos, yRandomPos, 2);
        return randomPos;
    }

    public virtual void Heal()
    {
        currentHealth++;
    }

    public virtual void Heal(float amount)
    {
        currentHealth += amount;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0) { Die(); }
    }

    protected virtual void DestroyEnemy()
    {
        Die();
    }

    public virtual void ResetEnemy()
    {
        currentHealth = enemyData.health;
    }
}
