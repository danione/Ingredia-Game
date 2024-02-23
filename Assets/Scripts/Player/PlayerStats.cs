using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IUnitStats
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxArmour;
    [SerializeField] private float health;
    [SerializeField] private float healthRegenTime;
    [SerializeField] private int healthRegenRate = 1;
    public float Health => health;
    private float armour = 0;

    private bool isHealing = false;
    private bool isArmourEnabled = false;

    private Coroutine combatCoroutine;
    private bool isInCombat = false;
    private float combatSeconds = 3;

    private void Start()
    {
        health = startingHealth;
        PlayerEventHandler.Instance.AdjustHealth();
        EnableArmour();
    }

    public void Die()
    {
        GameManager.Instance.gameOver = true;
    }

    // Disable healing potions and start the self-healing process
    public void SetPermanentHealing()
    {
        isHealing = true;
        StartCoroutine(PermaHealing());
    }

    // Self-Healing Upgrade Unlock
    // If not in combat, heal up to the max health every n-th seconds
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

    // Health potions heal over a set time
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

    public void EnableArmour()
    {
        isArmourEnabled = true;
        armour = maxArmour;
    }

    public void UpgradeHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        Heal(newMaxHealth);
    }

    public void RepairAmour(float amount)
    {
        armour = Mathf.Min(maxArmour, armour + amount);
    }

    public void Heal(float amount)
    {
        if (amount < 0 || isHealing) return;

        isHealing = true;
        float healIncrease = Mathf.Min(health + amount, maxHealth);
        StartCoroutine(HealOverTime(healIncrease));
    }

    public void TakeDamage(float amount)
    {
        if (isArmourEnabled)
        {
            armour -= amount;
            if(armour < 0)
            {
                health += armour;
                armour = 0;
            }
        }
        else
        {
            health -= amount;
        }

        PlayerEventHandler.Instance.AdjustHealth();

        if (isInCombat)
        {
            StopCoroutine(combatCoroutine);
        }

        combatCoroutine = StartCoroutine(CombatTimer());


        if (Health <= 0) { Die(); }
    }

    private IEnumerator CombatTimer()
    {
        isInCombat = true;
        yield return new WaitForSeconds(combatSeconds);
        isInCombat = false;
    }
}
