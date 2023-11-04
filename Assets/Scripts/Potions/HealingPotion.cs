using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : IPotion
{
    private bool destroyed = false;
    public bool Destroyed => destroyed;
    private int healAmount = 1;

    public void Destroy()
    {
        destroyed = true;
    }

    public void Tick()
    {
        Destroy();
    }

    public void Use()
    {
        PlayerController.Instance.stats.Heal(healAmount);
    }

    public void Reset()
    {
        destroyed = false;
    }
}
