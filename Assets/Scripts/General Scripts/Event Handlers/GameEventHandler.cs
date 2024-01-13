using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    public static GameEventHandler Instance;
    public Action<Vector3> DestroyedEnemy;
    public Action<Vector3> FuseBats;
    public Action BansheeDetectedPlayer;
    public Action UpgradesMenuOpen;
    public Action UpgradesMenuClose;
    public Action<int, Vector3> SpawnGoldenNugget;
    public Action<GhostPotionData> ActivatedGhost;
    public Action<OverloadElixirData> ActivatedLaser;
    public Action<float> ActivatedBarrier;
    public Action ActivatedSmartRitualHelper;
    public Action<Ritual> CollectedExistingIngredient;
    public Action<IngredientData> HighlightedIngredient;
    public Action<Vector3> GeneratedIngredientAtPos;
    public Action<GameObject> DestroyedObject;
    public Action UnlockedScrollSlip;
    public Action<RitualScriptableObject> ScrollSlipGenerated;
    public Action<Vector3> SpawnAScrollSlip;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void DestroyEnemy(Vector3 pos)
    {
        DestroyedEnemy?.Invoke(pos);
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
                DestroyObject(obj);
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

    public void ActivateBarrier(float duration)
    {
        ActivatedBarrier?.Invoke(duration);
    }

    public void ActivateSmartRitualHelper()
    {
        ActivatedSmartRitualHelper?.Invoke();
    }

    public void CollectExistingIngredient(Ritual ritual)
    {
        CollectedExistingIngredient?.Invoke(ritual);
    }

    public void HighlightIngredient(IngredientData ingredient)
    {
        HighlightedIngredient?.Invoke(ingredient);
    }

    public void GenerateIngredientAtPos(Vector3 pos)
    {
        GeneratedIngredientAtPos?.Invoke(pos);
    }

    public void DestroyObject(GameObject obj)
    {
        DestroyedObject?.Invoke(obj);
    }

    public void UnlockScrollSlip()
    {
        UnlockedScrollSlip?.Invoke();
    }

    public void ScrollSlipGenerate(RitualScriptableObject scroll)
    {
        ScrollSlipGenerated?.Invoke(scroll);
    }

    public void CallToSpawnASlip(Vector3 position)
    {
        SpawnAScrollSlip?.Invoke(position);
    }
}
