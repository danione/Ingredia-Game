using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private List<Product> uniqueEnemies;

    [SerializeField] private List<SpawnStage> stage;
    [SerializeField] private float spawnFrequencyInSeconds = 2.0f;
    [SerializeField] private float waveSpawnCooldownInSeconds = 3.0f;
    [SerializeField] private Product upgradedBat;

    private Dictionary<Product, ObjectsSpawner> spawner = new();

    [SerializeField] private int currentAliveEnemies = 0;
    [SerializeField] private SpawnPointManager spawnPointManager;
    [SerializeField] private BoundariesData boundariesData;
    [SerializeField] private float dequeueTime;

    [SerializeField] private int currentStageIndex = 0;
    private List<int> currentStage = new();

    private bool hasSpawnedAll = false;
    private bool isNextStageTransitioning = false;


    void Start()
    {
        spawnPointManager = new SpawnPointManager(boundariesData,dequeueTime);
        foreach (var enemy in uniqueEnemies)
        {
            spawner[enemy] = new ObjectsSpawner(enemy);
        }

        InitNextStage();
        StartCoroutine(spawnPointManager.ResetNextPoint());
        GameEventHandler.Instance.DestroyedEnemy += OnEnemyDestroyed;
        GameEventHandler.Instance.FuseBats += OnFusedTwoBats;

        StartCoroutine(WaitEnd());
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.DestroyedEnemy -= OnEnemyDestroyed;
            GameEventHandler.Instance.FuseBats -= OnFusedTwoBats;
            StopAllCoroutines();
        }
        catch { }

    }

    // Done for UI, the StageChange function is only sent to it
    // This function is added since the UI is not updating
    private IEnumerator WaitEnd()
    {
        yield return new WaitForEndOfFrame();
        GameEventHandler.Instance.StageChange(currentStageIndex);
        StartCoroutine(SpawnEnemies());
    }


    // Initialises the next stage
    // ---
    // Clears the current stage info, for each enemy in the next stage,
    // add their count
    private void InitNextStage()
    {
        if (currentStageIndex >= stage.Count) return;

        GameEventHandler.Instance.StageChange(currentStageIndex);

        hasSpawnedAll = false;

        currentStage.Clear();
        
        try
        {
            foreach (var enemy in stage[currentStageIndex].enemyList)
            {
                currentStage.Add(enemy.amount);
            }
            isNextStageTransitioning = false;
        }
        catch (Exception ex) { Debug.LogError(ex); }
    }

    private void OnEnemyDestroyed(Vector3 pos)
    {
        if(currentAliveEnemies > 0)
            currentAliveEnemies--;
        
        if (currentAliveEnemies == 0 && !isNextStageTransitioning)
        {
            isNextStageTransitioning = true;
            StartCoroutine(TransitionToNextStage());
        }
    }

    private IEnumerator TransitionToNextStage()
    {
        yield return new WaitForSeconds(waveSpawnCooldownInSeconds);
        currentStageIndex++;
        InitNextStage();
    }


    // Spawns ingredients at random times
    private IEnumerator SpawnEnemies()
    {
        while (!GameManager.Instance.gameOver)
        {
            yield return new WaitForSeconds(spawnFrequencyInSeconds);
            if (!hasSpawnedAll)
            {
                Debug.Log("Hello");
                SpawnEnemy();
            }
        }
        yield return null;
    }

    private void SpawnEnemy()
    {
        CheckIfAllSpawned();
        if (uniqueEnemies.Count == 0 || currentStage.Count == 0 || hasSpawnedAll) return;

        if (stage[currentStageIndex].isRandom)
        {
            int randomEnemyIndex = UnityEngine.Random.Range(0, currentStage.Count);

            while(currentStage[randomEnemyIndex] <= 0 && !hasSpawnedAll)
            {
                randomEnemyIndex = UnityEngine.Random.Range(0, currentStage.Count);
            }
            SpawnAtPos(randomEnemyIndex);
        }
        else
        {
            SpawnAtPos();
        }
    }

    private void SpawnAtPos(int index = 0)
    {
        if (currentStage[index] <= 0)
        {
            return;
        }
        Product enemy = stage[currentStageIndex].enemyList[index].enemy;
        Vector3 position = GetRandomPosition();

        if (position == Vector3.zero) return;

        spawner[enemy].GetProduct(position);
        currentAliveEnemies++;
        currentStage[index]--;
    }

    private Vector3 GetRandomPosition()
    {
        SpawnPoint xPoint = spawnPointManager.PickNewRandomPoint(isXPoint: true);
        SpawnPoint yPoint = spawnPointManager.PickNewRandomPoint(isXPoint: false);

        if (xPoint == null || yPoint == null) return Vector3.zero;

        return new(xPoint.pos, yPoint.pos, 2);
    }

    private void CheckIfAllSpawned()
    {
        if (hasSpawnedAll) return;

        foreach(var enemyCount in currentStage)
        {
            if (enemyCount > 0)
                return;
        }
        hasSpawnedAll = true;
    }

    private void OnFusedTwoBats(Vector3 position)
    {
        Product product = spawner[upgradedBat]._pool.Get();
        product.gameObject.transform.position = position;
        currentAliveEnemies++;
    }

    public void SetTutorialCurrency()
    {
        hasSpawnedAll = true;
        currentAliveEnemies = 10;
    }
}

