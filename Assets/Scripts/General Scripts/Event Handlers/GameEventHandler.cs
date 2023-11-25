using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    public static GameEventHandler Instance;
    public Action DestroyedEnemy;
    public Action<Vector3> FuseBats;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroyEnemy()
    {
        DestroyedEnemy?.Invoke();
    }

    public void FuseTwoBats(Vector3 upgradedBatPosition)
    {
        FuseBats?.Invoke(upgradedBatPosition);
    }
}
