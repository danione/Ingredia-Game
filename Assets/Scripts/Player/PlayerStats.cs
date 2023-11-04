using UnityEngine;

public class PlayerStats : MonoBehaviour, IUnitStats
{
    [SerializeField] private int startingHealth;
    [SerializeField] private int maxHealth;

    [SerializeField] private float health;
    public float Health => health;

    private void Start()
    {
        health = startingHealth;
    }

    public void Die()
    {
        GameManager.Instance.gameOver = true;
        Debug.Log("Dead.");
    }

    public void Heal()
    {
        if (Health < maxHealth)
        {
            health++;
        }

    }

    public void UpgradeHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        Heal(newMaxHealth);
    }

    public void Heal(int _health)
    {
        health = Mathf.Min(health + _health, maxHealth);
        Debug.Log("Health is " + health);
    }

    public void TakeDamage()
    {
        health--;

        Debug.Log("Ugh, taken damage " + Health);
        if (Health < 1) { Die(); }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        Debug.Log("Ugh, taken damage " + Health);
        if (Health < 1) { Die(); }
    }
}
