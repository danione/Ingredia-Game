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
    [SerializeField] private GameObject upgradedBat;
    private List<ObjectsSpawner> spawner = new();

    private int currentAliveEnemies = 0;

    void Start()
    {
        foreach (var enemy in enemies)
        {
            spawner.Add(new ObjectsSpawner(enemy.enemy));
        }
        StartCoroutine(SpawnEnemies());
        GameEventHandler.Instance.DestroyedEnemy += OnEnemyDestroyed;
        GameEventHandler.Instance.FuseBats += OnFusedTwoBats;
        currentWave = 1;
    }

    private void OnEnemyDestroyed()
    {
        if(currentAliveEnemies > 0)
            currentAliveEnemies--;
        if (destroyCancellationToken.IsCancellationRequested) return;
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
                spawner[index]._pool.Get().gameObject.transform.position = pos;
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
        GameObject bat = Instantiate(upgradedBat, position, Quaternion.identity);
        bat.GetComponent<BatEnemy>().Upgrade();
        currentAliveEnemies++;
    }
}

[System.Serializable]
public class WaveEnemy
{
    public Product enemy;
    public int cost;
}
