using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenNuggetsFactory : MonoBehaviour
{
    [SerializeField] private ObjectsSpawner spawner;

    private void Start()
    {
        GameEventHandler.Instance.SpawnGoldenNugget += OnSpawnGoldenNugget;
    }

    private void OnSpawnGoldenNugget(int value, Vector3 position)
    {
        Product product = spawner._pool.Get();
        product.transform.position = position;
        product.GetComponent<GoldenNuggets>().Amount = value;        
    }
}
