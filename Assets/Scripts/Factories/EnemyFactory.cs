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

    private int currentAliveEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        GameEventHandler.Instance.DestroyedEnemy += OnEnemyDestroyed;
        currentWave = 1;
    }

    private void OnEnemyDestroyed()
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
            int index = Random.Range(0, enemies.Count);
            if (currentCurrency - enemies[index].cost >= 0)
            {
                currentCurrency -= enemies[index].cost;
                Enemy within = enemies[index].enemy.GetComponent<Enemy>();
                Instantiate(enemies[index].enemy, within.GetRandomPosition(), Quaternion.identity);
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
        Debug.Log("Welcome");
        yield return new WaitForSeconds(waveSpawnCooldownInSeconds);

        currentWave++;
        currentCurrency += currentWave;
    }
}

[System.Serializable]
public class WaveEnemy
{
    public MonoBehaviour enemy;
    public int cost;
}
