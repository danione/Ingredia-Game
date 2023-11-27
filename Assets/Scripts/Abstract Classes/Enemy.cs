using UnityEngine;

public abstract class Enemy : MonoBehaviour, IUnitStats
{
    [SerializeField] private float health;
    public float Health => health;
    [SerializeField] public float xLeftMaxSpawn;
    [SerializeField] public float xRightMaxSpawn;
    [SerializeField] protected float yTopMaxSpawn;
    [SerializeField] protected float yBottomMaxSpawn;

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual Vector3 GetRandomPosition()
    {
        float xRandomPos = Random.Range(xLeftMaxSpawn, xRightMaxSpawn);
        float yRandomPos = Random.Range(yTopMaxSpawn, yBottomMaxSpawn);
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
        if(GameEventHandler.Instance != null)
            GameEventHandler.Instance.DestroyEnemy();
        DestroyEnemy();
        
    }

    protected abstract void DestroyEnemy();
}
