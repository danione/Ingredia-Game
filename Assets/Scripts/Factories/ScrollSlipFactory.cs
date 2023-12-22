using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSlipFactory : MonoBehaviour
{
    [SerializeField] private Product scrollSlipPrefab;
    [SerializeField, Range(0.0f, 1.0f)] private float spawnChance;
    [SerializeField] private GameObject scrollSlipManagerRef;

    private ObjectsSpawner spawner;
    private ScrollSlipManager scrollSlipManager;

    private void Start()
    {
        spawner = new ObjectsSpawner(scrollSlipPrefab);
        scrollSlipManager = scrollSlipManagerRef.GetComponent<ScrollSlipManager>();
        GameEventHandler.Instance.DestroyedEnemy += OnEnemyKilled;
    }

    private void OnEnemyKilled(Vector3 position)
    {
        if (Random.Range(0.0f, 1.0f) > spawnChance || scrollSlipManager.GetAvailableSlipsCount() == 0) return;

        spawner.GetProduct(position);
    }

}
