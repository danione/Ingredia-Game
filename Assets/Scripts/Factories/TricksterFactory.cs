using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterFactory : MonoBehaviour
{
    [SerializeField] private Product tricksterProjectile;
    [SerializeField] private ObjectsSpawner spawner;

    private void Start()
    {
        spawner = new ObjectsSpawner(tricksterProjectile);
        GameEventHandler.Instance.SpawnedATricksterProjectileAt += OnSpawnProjectileAt;
    }

    private void OnSpawnProjectileAt(Vector3 pos, TricksterEnemy spawnedBy)
    {
        Product projectile = spawner.GetProduct(pos);
        projectile.GetComponent<FallableObject>().SwapToCirculate(spawnedBy.transform, new Vector3(0,0, Random.Range(0, 2) * 2 - 1)); // Giving -1 or 1
        spawnedBy.AddCapturedProjectile(projectile);
    }
}
