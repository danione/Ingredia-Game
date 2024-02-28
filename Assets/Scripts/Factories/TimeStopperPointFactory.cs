using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopperPointFactory : MonoBehaviour
{
    [SerializeField] private Product timeStopperPoint;
    [SerializeField] private ObjectsSpawner spawner;

    private void Start()
    {
        spawner = new ObjectsSpawner(timeStopperPoint);
        GameEventHandler.Instance.SpawnedTimeStopPoint += OnSpawnedTimeStopPoint;
    }

    private void OnSpawnedTimeStopPoint(Vector3 pos, GameObject source)
    {

        Product projectile = spawner.GetProduct(pos);
        projectile.GetComponent<TimeStopperPoint>().Init(source);
    }

}
