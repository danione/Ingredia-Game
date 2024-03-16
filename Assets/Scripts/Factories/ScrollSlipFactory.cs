using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollSlipFactory : MonoBehaviour
{
    [SerializeField] private Product scrollSlipPrefab;
    [SerializeField, Range(0.0f, 1.0f)] private float spawnChance;

    private ObjectsSpawner spawner;
    private ScrollSlipManager scrollSlipManager;

    private void Start()
    {
        spawner = new ObjectsSpawner(scrollSlipPrefab);
        scrollSlipManager = GameManager.Instance.GetComponent<ScrollSlipManager>();
        GameEventHandler.Instance.DestroyedEnemy += OnEnemyKilled;
        GameEventHandler.Instance.SpawnAScrollSlip += SpawnAScrollSlipAtLocation;
    }

    private void OnEnemyKilled(Vector3 position)
    {
        if (Random.Range(0.0f, 1.0f) > spawnChance || scrollSlipManager.GetAvailableSlipsCount() == 0) return;

        spawner.GetProduct(position);
    }

    private void SpawnAScrollSlipAtLocation(Vector3 location)
    {
        spawner.GetProduct(location);
    }

}
