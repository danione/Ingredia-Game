using UnityEngine;

public class PlayerStats : MonoBehaviour, IUnitStats
{
    [SerializeField] private int startingHealth;
    public int Health { get; private set; }

    private void Awake()
    {
        Health = startingHealth;
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
            Health++;
        }

    }

    public void Heal(int health)
    {
        Health = Mathf.Min(Health + health, startingHealth);
    }

    public void TakeDamage()
    {
        Health--;

        Debug.Log("Ugh, taken damage " + Health);
        if (Health < 1) { Die(); }
    }
}
