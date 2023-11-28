using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    public static GameEventHandler Instance;
    public Action DestroyedEnemy;
    public Action<Vector3> FuseBats;
    public Action BansheeDetectedPlayer;
    public Action UpgradesMenuOpen;
    public Action UpgradesMenuClose;
    public Action<int, Vector3> SpawnGoldenNugget;
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

    public void BansheeDetectPlayer()
    {
        BansheeDetectedPlayer?.Invoke();
    }

    public void BringUpUpgradesMenu()
    {
        UpgradesMenuOpen?.Invoke();
    }

    public void ClosingDownUpgradesMenu()
    {
        UpgradesMenuClose?.Invoke();
    }

    public void SpawnsGoldenNuggets(int amount, Vector3 position)
    {
        SpawnGoldenNugget?.Invoke(amount, position);
    }
}
