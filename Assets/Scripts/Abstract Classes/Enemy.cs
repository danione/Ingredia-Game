using UnityEngine;

public abstract class Enemy : MonoBehaviour, IUnitStats
{
    [SerializeField] private float health;
    [SerializeField] private int goldRewardOnDeath;
    public float Health => health;
    [SerializeField] protected BoundariesData spawnBoundaries;

    public virtual void Die()
    {
        GameEventHandler.Instance.SpawnsGoldenNuggets(goldRewardOnDeath, gameObject.transform.position);
        GameEventHandler.Instance.DestroyObject(gameObject);
    }

    public BoundariesData Boundaries => spawnBoundaries;

    public virtual Vector3 GetRandomPosition()
    {
        float xRandomPos = Random.Range(spawnBoundaries.xLeftMax, spawnBoundaries.xRightMax);
        float yRandomPos = Random.Range(spawnBoundaries.yTopMax, spawnBoundaries.yBottomMax);
        Vector3 randomPos = new Vector3(xRandomPos, yRandomPos, 2);
        return randomPos;
    }

    public virtual void Heal()
    {
        health++;
    }

    public virtual void Heal(float amount)
    {
        health += amount;
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0) { Die(); }
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.DestroyEnemy();
        DestroyEnemy();
    }

    protected abstract void DestroyEnemy();
}
