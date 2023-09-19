using UnityEngine;

public class PlayerStats : MonoBehaviour, IUnitStats
{
    [SerializeField] private int startingHealth;

    private float health;
    public float Health => health;

    private void Awake()
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
        if (Health < startingHealth)
        {
            health++;
        }

    }

    public void Heal(int _health)
    {
        health = Mathf.Min(health + _health, startingHealth);
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
