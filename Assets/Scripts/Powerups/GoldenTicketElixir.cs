using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenTicketElixir : IPowerUp
{
    private bool destroyed = false;
    public bool Destroyed => destroyed;
    private Transform goldenNugget;

    public GoldenTicketElixir(Transform goldenNugget)
    {
        this.goldenNugget = goldenNugget;
    }

    public void Destroy()
    {
        destroyed = true;
    }

    public void Tick()
    {
        // Nothing
    }

    private GameObject[] GetAllObjectsWithTag(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag);
    }

    private void ConvertToGoldenNugget(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            GameObject.Instantiate(goldenNugget, obj.transform.position, goldenNugget.transform.rotation);
            GameObject.Destroy(obj);
        }
        
    }

    public void Use()
    {
        if (destroyed) return;

        string[] tags = { "Projectile", "Ingredient", "Recipe" };

        foreach (string tag in tags)
        {
            ConvertToGoldenNugget(GetAllObjectsWithTag(tag));
        }

        Destroy();
    }
}
