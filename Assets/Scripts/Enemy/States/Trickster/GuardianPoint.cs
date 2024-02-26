using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianPoint : MonoBehaviour, IUnitStats
{
    public float health;
    public float Health => health;

    public void Die()
    {
        GameEventHandler.Instance.PointDestroy(this);
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
