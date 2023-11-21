using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelSpitterElixir : IPotion
{
    private bool destroyed = false;
    public bool Destroyed => destroyed;
    private int ammoToAdd = 5;

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
        PlayerController.Instance.inventory.AddKnifeAmmo(ammoToAdd);
    }

    public void Reset()
    {
        destroyed = false;
    }
}
