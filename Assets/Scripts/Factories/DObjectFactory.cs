using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DObjectFactory : MonoBehaviour
{
    [SerializeField] private SpawnLocationData spawnLocation;
    [SerializeField] private float spawnFrequency;
    [SerializeField] private Product dangerousObject;
    [SerializeField] private ObjectsSpawner spawner;

    private void Start()
    {
        spawner = new ObjectsSpawner(dangerousObject);
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while (!GameManager.Instance.gameOver)
        {
            Vector3 position = new Vector3(Random.Range(spawnLocation.xLeftMax, spawnLocation.xRightMax), spawnLocation.yLocation, 2);

            Product DObj = spawner._pool.Get();
            DObj.gameObject.transform.position = position;
            DObj.GetComponent<FallableObject>().SwapToMove();

            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
