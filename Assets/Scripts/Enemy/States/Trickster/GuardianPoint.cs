using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GuardianPoint : MonoBehaviour, IUnitStats
{
    public int index;
    private float defaultHealth;
    public float health;
    public float Health => health;
    private bool isDead = false;

    public void Init(float health)
    {
        isDead = false;
        defaultHealth = health;
        GameEventHandler.Instance.DestroyedObject += OnDestroyed;
        Heal(defaultHealth);
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.DestroyedObject -= OnDestroyed;

        }
        catch { }
    }

    // When the parent object has died, it needs to destroy all its points
    public void OnDestroyed(GameObject obj)
    {
        if (!obj.GetComponent<GuardianEnemy>()) return;

        if (this != null && obj == gameObject.transform.parent)
        {
            isDead = true;
            try
            {
                GameEventHandler.Instance.DestroyedObject -= OnDestroyed;

            }
            catch { }
        }
    }

    // When this particular object has died
    public void Die()
    {
        if (isDead) return;

        isDead = true;
        GameEventHandler.Instance.PointDestroy(this);
    }

    public void Revive()
    {
        isDead = false;
        Heal(defaultHealth);
    }

    public void Heal(float amount)
    {
        health = amount;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }
}
