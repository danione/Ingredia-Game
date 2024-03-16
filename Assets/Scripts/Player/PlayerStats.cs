using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IUnitStats
{
    [SerializeField] private float defaultStartingHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxArmour;
    [SerializeField] private float healthRegenTime;
    [SerializeField] private int healthRegenRate = 1;
    private float health;

    public float Health => health;
    public float Armour => armour;
    private float armour = 0;

    private bool isHealing = false;
    private bool isArmourEnabled = false;

    private Coroutine combatCoroutine;
    private bool isInCombat = false;
    private bool isSelfRegenArmour = false;
    private float combatSeconds = 3;

    private void Start()
    {
        health = defaultStartingHealth;
        SceneManager.sceneUnloaded += OnLevelUnloaded;
    }

    private void OnDestroy()
    {
        try
        {
            SceneManager.sceneUnloaded -= OnLevelUnloaded;

        }
        catch { }
    }

    private void OnLevelUnloaded(Scene a)
    {
        if (a.name == "Normal Level")
        {
            isArmourEnabled = false;
            StopCoroutine(PermaHealing());
            armour = 0;
            isSelfRegenArmour = false;
        }
        maxHealth = defaultStartingHealth;
        health = defaultStartingHealth;
        PlayerEventHandler.Instance.AdjustHealth();
    }

    public void Die()
    {
        PlayerEventHandler.Instance.PlayerDies();
        GameManager.Instance.gameOver = true;
    }

    // Disable healing potions and start the self-healing process
    public void SetPermanentHealing()
    {
        isHealing = true;
        StartCoroutine(PermaHealing());
    }

    public void SetAutoRegenArmour()
    {
        isSelfRegenArmour = true;
    }

    // Self-Healing Upgrade Unlock
    // If not in combat, heal up to the max health every n-th seconds
    private IEnumerator PermaHealing()
    {
        while(!GameManager.Instance.gameOver)
        {
            if (!isInCombat)
            {
                if (health < maxHealth)
                {
                    health = Math.Min(health + healthRegenRate, maxHealth);
                    PlayerEventHandler.Instance.AdjustHealth();

                }
                else if(isSelfRegenArmour && armour < maxArmour)
                {
                    armour = Math.Min(armour + healthRegenRate, maxArmour);
                    PlayerEventHandler.Instance.AdjustHealth();
                }
            }
            yield return new WaitForSeconds(healthRegenTime);
        }
    }

    // Health potions heal over a set time
    private IEnumerator HealOverTime(float healthIncrease)
    {
        while(health < healthIncrease)
        {
            yield return new WaitForSeconds(healthRegenTime);
            health = Math.Min(health + healthRegenRate, healthIncrease);
            PlayerEventHandler.Instance.AdjustHealth();
        }

        isHealing = false;
    }

    public void EnableArmour()
    {
        isArmourEnabled = true;
        armour = maxArmour;
        PlayerEventHandler.Instance.AdjustHealth();
    }

    public void UpgradeHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        health = newMaxHealth;
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
