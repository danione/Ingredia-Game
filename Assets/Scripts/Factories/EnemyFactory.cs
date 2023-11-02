using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private List<WaveEnemy> enemies;
    [SerializeField] private int currentCurrency = 0;
    [SerializeField] private float spawnFrequencyInSeconds = 2.0f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void SpawnEnemy()
    {
        if (enemies.Count == 0) return;

        while (true)
        {
            int index = Random.Range(0, enemies.Count);
            if (currentCurrency - enemies[index].cost >= 0)
            {
                currentCurrency -= enemies[index].cost;
                Enemy within = enemies[index].enemy.GetComponent<Enemy>();
                Instantiate(enemies[index].enemy, within.GetRandomPosition(), Quaternion.identity);
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
}

[System.Serializable]
public class WaveEnemy
{
    public MonoBehaviour enemy;
    public int cost;
}
