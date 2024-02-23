using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IUnitStats
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float healthRegenTime;
    [SerializeField] private int healthRegenRate = 1;
    public float Health => health;

    private bool isHealing = false;

    private void Start()
    {
        health = startingHealth;
        PlayerEventHandler.Instance.AdjustHealth();
    }

    public void Die()
    {
        GameManager.Instance.gameOver = true;
    }

    public void HealInstantly(float healAmount)
    {
        health = Mathf.Min(health + healAmount, maxHealth);
        PlayerEventHandler.Instance.AdjustHealth();
    }

    private IEnumerator HealOverTime(float healthIncrease)
    {
        while(health < healthIncrease)
        {
            health = Math.Min(health + healthRegenRate, healthIncrease);
            PlayerEventHandler.Instance.AdjustHealth();
            yield return new WaitForSeconds(healthRegenTime);
        }
        isHealing = false;
    }

    public void UpgradeHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        Heal(newMaxHealth);
    }

    public void Heal(float amount)
    {
        if (amount < 0 || isHealing) return;

        isHealing = true;
        float healIncrease = Mathf.Min(health + amount, maxHealth);
        StartCoroutine(HealOverTime(healIncrease));
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
