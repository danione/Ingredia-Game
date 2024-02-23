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

    public void SetPermanentHealing()
    {
        isHealing = true;
        StartCoroutine(PermaHealing());
    }

    private IEnumerator PermaHealing()
    {
        while(!GameManager.Instance.gameOver)
        {
            if (!isInCombat && health < maxHealth)
            {
                health = Math.Min(health + healthRegenRate, maxHealth);
                PlayerEventHandler.Instance.AdjustHealth();
            }
            yield return new WaitForSeconds(healthRegenTime);
        }
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

    private Coroutine combatCoroutine;
    private bool isInCombat = false;
    private float combatSeconds = 3;

    public void TakeDamage(float amount)
    {
        health -= amount;
        PlayerEventHandler.Instance.AdjustHealth();

        if (isInCombat)
        {
            StopCoroutine(combatCoroutine);
        }

        combatCoroutine = StartCoroutine(CombatTimer());


        if (Health < 1) { Die(); }
    }

    private IEnumerator CombatTimer()
    {
        isInCombat = true;
        yield return new WaitForSeconds(combatSeconds);
        isInCombat = false;
    }
}
