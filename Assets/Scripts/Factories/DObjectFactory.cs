using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DObjectFactory : MonoBehaviour
{
    [SerializeField] private SpawnPointsScriptableObject spawnLocation;
    [SerializeField] private float spawnFrequency;

    [SerializeField] private GameObject dangerousObject;

    private void Start()
    {

        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while (!GameManager.Instance.gameOver)
        {
            Vector3 position = new Vector3(Random.Range(spawnLocation.xLeftMax, spawnLocation.xRightMax), spawnLocation.yLocation, 2);
            Instantiate(dangerousObject, position, Quaternion.identity);
            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
