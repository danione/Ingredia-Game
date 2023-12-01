using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
    public Action<GhostPotionData> ActivatedGhost;
    public Action<OverloadElixirData> ActivatedLaser;

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

    public void ConvertingAllObjectsToGoldenNuggets(int amount, List<string> tags)
    {        
        foreach (string tag in tags)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                SpawnsGoldenNuggets(amount, obj.gameObject.transform.position);
                Destroy(obj);
            }
        }
    }

    public void ActivateGhost(GhostPotionData data)
    {
        ActivatedGhost?.Invoke(data);
    }

    public void ActivateLaser(OverloadElixirData data)
    {
        ActivatedLaser?.Invoke(data);
    }
}
