using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private List<WaveEnemy> enemies;
    [SerializeField] private float currentCurrency = 0;
    [SerializeField] private float spawnFrequencyInSeconds = 2.0f;
    [SerializeField] private float waveSpawnCooldownInSeconds = 3.0f;
    [SerializeField] private float currentWave;
    [SerializeField] private Product upgradedBat;

    private List<ObjectsSpawner> spawner = new();
    private ObjectsSpawner upgradedSpawner;

    private int currentAliveEnemies = 0;

    void Start()
    {
        foreach (var enemy in enemies)
        {
            spawner.Add(new ObjectsSpawner(enemy.enemy));
        }
        upgradedSpawner = new ObjectsSpawner(upgradedBat);

        StartCoroutine(SpawnEnemies());
        GameEventHandler.Instance.DestroyedEnemy += OnEnemyDestroyed;
        GameEventHandler.Instance.FuseBats += OnFusedTwoBats;
        currentWave = 1;
    }

    private void OnEnemyDestroyed(Vector3 pos)
    {
        if(currentAliveEnemies > 0)
            currentAliveEnemies--;
        if (currentAliveEnemies == 0 && currentCurrency == 0)
            StartCoroutine(IncrementWave());
    }

    private void SpawnEnemy()
    {
        if (enemies.Count == 0) return;

        while (!GameManager.Instance.gameOver)
        {
            int index = UnityEngine.Random.Range(0, enemies.Count);
            if (currentCurrency - enemies[index].cost >= 0)
            {
                currentCurrency -= enemies[index].cost;
                Enemy enemy = enemies[index].enemy.GetComponent<Enemy>();

                if (enemy == null) continue;
                
                Vector3 pos = enemy.GetRandomPosition();
                Product product = spawner[index]._pool.Get();
                product.gameObject.transform.position = pos;
                product.GetComponent<Enemy>().ResetEnemy();

                currentAliveEnemies++;
                break;
            }
        }
    }

        // Spawns ingredients at random times
    private IEnumerator SpawnEnemies()
    {
        while (!GameManager.Instance.gameOver)
        {
            if(currentCurrency > 0)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnFrequencyInSeconds);

        }
        yield return null;
    }

    private IEnumerator IncrementWave()
    {
        yield return new WaitForSeconds(waveSpawnCooldownInSeconds);
        currentWave++;
        currentCurrency += currentWave;
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
        currentCurrency = i;
    }
}

[System.Serializable]
public class WaveEnemy
{
    public Product enemy;
    public int cost;
}
