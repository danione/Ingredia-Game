using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DObjectFactory : MonoBehaviour
{
    [SerializeField] private float xBorderLeft;
    [SerializeField] private float xBorderRight;
    [SerializeField] private float yPosition;
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
            Vector3 position = new Vector3(Random.Range(xBorderLeft, xBorderRight), yPosition, 2);
            Instantiate(dangerousObject, position, Quaternion.identity);
            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
