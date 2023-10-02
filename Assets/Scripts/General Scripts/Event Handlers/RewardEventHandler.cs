using System;
using UnityEngine;

public class RewardEventHandler : MonoBehaviour
{
    public static RewardEventHandler Instance;

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
}
