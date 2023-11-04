using System;
using UnityEngine;

public class RewardEventHandler : MonoBehaviour
{
    public static RewardEventHandler Instance;

    [SerializeField] private PlayerController player;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
/*
    public void PlayerReward(IReward reward)
    {
        reward.GrantReward(player);
    }*/

}
