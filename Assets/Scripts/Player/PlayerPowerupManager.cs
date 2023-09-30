using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupManager: MonoBehaviour
{
    private List<IPowerUp> powerupsInUse = new List<IPowerUp>();
    private Stack<IPowerUp> powerupsInInventory = new Stack<IPowerUp>();
    [SerializeField] private Transform goldenNuggets;


    private void Start()
    {
        IPowerUp potion = new GoldenTicketElixir(goldenNuggets);
        potion.Use();
        powerupsInUse.Add(potion);
    }

    public void HandlePowerups()
    {
        
        if (powerupsInUse.Count == 0) return;
        for (int i = powerupsInUse.Count - 1; i >= 0; i--)
        {
            powerupsInUse[i].Tick();
            if (powerupsInUse[i].Destroyed)
            {
                powerupsInUse.RemoveAt(i);
            }
        }
    }

    public void AddPowerup(IPowerUp powerup)
    {
        powerupsInInventory.Push(powerup);
    }

    public void UsePowerup()
    {
        if(powerupsInInventory.Count > 0)
        {
            IPowerUp powerUp = powerupsInInventory.Pop();
            powerUp.Use();
            powerupsInUse.Add(powerUp);
        }
            
    }
}
