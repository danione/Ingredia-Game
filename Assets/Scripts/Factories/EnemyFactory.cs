using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private List<Product> uniqueEnemies;

    [SerializeField] private List<SpawnStage> stage;
    [SerializeField] private float spawnFrequencyInSeconds = 2.0f;
    [SerializeField] private float waveSpawnCooldownInSeconds = 3.0f;
    [SerializeField] private Product upgradedBat;

    private Dictionary<Product, ObjectsSpawner> spawner = new();
    private ObjectsSpawner upgradedSpawner;

    private int currentAliveEnemies = 0;
    private List<int> currentStage = new();
    private int currentStageIndex = 0;

    void Start()
    {
        foreach (var enemy in uniqueEnemies)
        {
            spawner[enemy] = new ObjectsSpawner(enemy);
        }

        SetNextStage();

        GameEventHandler.Instance.DestroyedEnemy += OnEnemyDestroyed;
        GameEventHandler.Instance.FuseBats += OnFusedTwoBats;

        StartCoroutine(SpawnEnemies());
    }

    private void SetNextStage()
    {
        if (currentStageIndex >= stage.Count) return;

        currentStage.Clear();
        
        try
        {
            foreach (var enemy in stage[currentStageIndex].enemyList)
            {
                currentStage.Add(enemy.amount);
            }
        }
        catch (Exception ex) { Debug.LogError(ex); }

    }

    private void OnEnemyDestroyed(Vector3 pos)
    {
        if(currentAliveEnemies > 0)
            currentAliveEnemies--;
        
        if (currentAliveEnemies == 0)
            StartCoroutine(NextStage());
    }

    private void SpawnEnemy()
    {
        if (uniqueEnemies.Count == 0) return;

        if (stage[currentStageIndex].isRandom)
        {
            int randomEnemyIndex = UnityEngine.Random.Range(0, currentStage.Count);
            SpawnEnemyAt(randomEnemyIndex);
        }
        else
        {
            SpawnEnemyAt();
        }
    }

    private void SpawnEnemyAt(int index = 0)
    {
        Product enemy = stage[currentStageIndex].enemyList[index].enemy;
        Vector3 position = enemy.GetComponent<Enemy>().GetRandomPosition();
        spawner[enemy].GetProduct(position);
        currentAliveEnemies++;
        currentStage[index]--;

        if (currentStage[index] <= 0)
            currentStage.Remove(index);
    }

        // Spawns ingredients at random times
    private IEnumerator SpawnEnemies()
    {
        while (!GameManager.Instance.gameOver)
        {
            if(currentStage.Count > 0)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnFrequencyInSeconds);

        }
        yield return null;
    }

    private IEnumerator NextStage()
    {
        yield return new WaitForSeconds(waveSpawnCooldownInSeconds);
        currentStageIndex++;
        SetNextStage();
    }

    private void OnFusedTwoBats(Vector3 position)
    {
        Product product = upgradedSpawner._pool.Get();
        product.gameObject.transform.position = position;
        product.GetComponent<BatEnemy>().Upgrade();
        currentAliveEnemies++;
    }

    public void SetTutorialCurrency(int i)
    {
        //currentCurrency = i;
    }
}

