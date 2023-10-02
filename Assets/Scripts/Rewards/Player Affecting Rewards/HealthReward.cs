using UnityEngine;

public class HealthReward : IReward
{
    private int amount;

    public HealthReward(int amount)
    {
        this.amount = amount;
    }
    public void GrantReward(MonoBehaviour entity)
    {
        PlayerStats playerStats = entity.GetComponent<PlayerStats>();
        if(playerStats != null)
        {
            playerStats.Heal(amount);
        }
    }
}
