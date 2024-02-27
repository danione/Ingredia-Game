using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianPoint : MonoBehaviour, IUnitStats
{
    public int index;
    private float defaultHealth;
    private float timeOfRegen;
    public float health;
    public float Health => health;
    private bool isDead = false;

    public void Init(float health, float timeOfRegen)
    {
        defaultHealth = health;
        this.timeOfRegen = timeOfRegen;
        GameEventHandler.Instance.DestroyedObject += OnDestroyed;
        Heal(defaultHealth);
    }

    public void OnDestroyed(GameObject obj)
    {
        if(obj == gameObject.transform.parent)
        {
            isDead = true;
            GameEventHandler.Instance.DestroyedObject -= OnDestroyed;
            StopCoroutine(Regenerate());
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        GameEventHandler.Instance.PointDestroy(this);
    }

    public IEnumerator Regenerate()
    {
        yield return new WaitForSeconds(timeOfRegen);
        isDead = false;
        GameEventHandler.Instance.PointRevive(this);
    }

    public void Revive()
    {
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
