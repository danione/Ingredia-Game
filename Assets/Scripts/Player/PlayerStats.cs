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
        PlayerEventHandler.Instance.AdjustHealth();
    }

    public void Die()
    {
        GameManager.Instance.gameOver = true;
    }

    public void Heal()
    { 
        if (Health < maxHealth)
        {
            health++;
        }
        PlayerEventHandler.Instance.AdjustHealth();
    }

    public void UpgradeHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        Heal(newMaxHealth);
    }

    public void Heal(int _health)
    {
        health = Mathf.Min(health + _health, maxHealth);
        PlayerEventHandler.Instance.AdjustHealth();
    }

    public void TakeDamage()
    {
        health--;
        PlayerEventHandler.Instance.AdjustHealth();
        if (Health < 1) { Die(); }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        PlayerEventHandler.Instance.AdjustHealth();
        if (Health < 1) { Die(); }
    }
}
