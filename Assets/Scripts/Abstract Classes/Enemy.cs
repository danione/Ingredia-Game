using UnityEngine;

public class Enemy : MonoBehaviour, IUnitStats
{
    private float health;
    public float Health => health;

    public virtual void Die()
    {
        Destroy(gameObject);
        Debug.Log("Drop loot");
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
