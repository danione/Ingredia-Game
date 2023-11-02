using UnityEngine;

public class Enemy : MonoBehaviour, IUnitStats
{
    private float health;
    public float Health => health;
    [SerializeField] protected float xLeftMaxSpawn;
    [SerializeField] protected float xRightMaxSpawn;
    [SerializeField] protected float yTopMaxSpawn;
    [SerializeField] protected float yBottomMaxSpawn;

    public virtual void Die()
    {
        Destroy(gameObject);
        Debug.Log("Drop loot");
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
        if(health < 0) { Die(); }
    }
}
