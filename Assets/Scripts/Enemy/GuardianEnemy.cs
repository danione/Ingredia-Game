using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianEnemy : Enemy
{
    private int count;
    [SerializeField] private int maxCount;
    private List<Vector3> guardianPoints = new();

    [SerializeField] private GameObject point;

    void Start()
    {

        // Sort points by x-coordinate
        guardianPoints.Sort((a, b) => a.x.CompareTo(b.x));

        // Ensure that the triangle covers a significant portion of the bounds
        Vector3 leftPoint = new Vector3(Random.Range(enemyData.spawnBoundaries.xRightMax, (enemyData.spawnBoundaries.xLeftMax + enemyData.spawnBoundaries.xRightMax)/2),
            Random.Range(enemyData.spawnBoundaries.yBottomMax, (enemyData.spawnBoundaries.yTopMax + enemyData.spawnBoundaries.yBottomMax)/2), 2);
        Vector3 rightPoint = new Vector3(Random.Range((enemyData.spawnBoundaries.xLeftMax + enemyData.spawnBoundaries.xRightMax) / 2, enemyData.spawnBoundaries.xLeftMax),
            Random.Range(enemyData.spawnBoundaries.yBottomMax, (enemyData.spawnBoundaries.yTopMax + enemyData.spawnBoundaries.yBottomMax) / 2), 2);
        Vector2 topPoint = new Vector3(Random.Range(enemyData.spawnBoundaries.xRightMax, enemyData.spawnBoundaries.xLeftMax),
            Random.Range((enemyData.spawnBoundaries.yTopMax + enemyData.spawnBoundaries.yBottomMax) / 2, enemyData.spawnBoundaries.yTopMax), 2);

        guardianPoints.Add(leftPoint);
        guardianPoints.Add(rightPoint);
        guardianPoints.Add(topPoint);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(point, guardianPoints[i], Quaternion.identity);
        }
    }
}
