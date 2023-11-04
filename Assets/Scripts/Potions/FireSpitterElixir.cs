using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpitterElixir : IPotion
{
    private bool destroyed = false;
    public bool Destroyed => destroyed;
    private int ammoToAdd = 30;

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
        PlayerController.Instance.inventory.AddAmmo(ammoToAdd);
    }

    public void Reset()
    {
        destroyed = false;
    }
}
