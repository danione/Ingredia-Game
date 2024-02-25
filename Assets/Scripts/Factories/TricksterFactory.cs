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
        projectile.GetComponent<SimpleProjectile>().SwapToFreeze();
       //projectile.GetComponent<FallableObject>().SwapToCirculate(spawnedBy.transform);
        spawnedBy.AddCapturedProjectile(projectile);
    }
}
