using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GuardianEnemy : Enemy
{
    [SerializeField] private int maxCount;
    [SerializeField] private int minCount;
    private List<GameObject> guardianPoints = new();
    private int activePoints = 3;
    List<Vector3> points;
    private LineRenderer lineRenderer;

    void Start()
    {
        Init();
        GameEventHandler.Instance.PointDestroyed += OnDestroyedObject;
    }

    private void OnDestroyedObject(GuardianPoint point)
    {
        if(activePoints > 0)
        {
            int index = guardianPoints.IndexOf(point.gameObject);
            if(index >= 0)
            {
                GameObject temp = guardianPoints[index];
                guardianPoints[index] = guardianPoints[activePoints - 1];
                guardianPoints[activePoints - 1] = temp;
                guardianPoints[activePoints - 1].SetActive(false);
                activePoints--;
            }
        }
    }

    private void Init()
    {
        SetupGuardianPoints();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4;
    }

    // Generates a random location within bounds
    // ---
    // The top we want top be in the top portion - say from (-6,6) to (6,0)
    // Left and right just in those portions - L -> (-6,0) to (0,-6) 
    // R -> (0,0) to (6, -6)

    private void SetupGuardianPoints()
    {
        // Ensure that the triangle covers a significant portion of the bounds
        Vector3 leftPoint = new Vector3(Random.Range(enemyData.spawnBoundaries.xRightMax, (enemyData.spawnBoundaries.xLeftMax + enemyData.spawnBoundaries.xRightMax) / 2),
            Random.Range(enemyData.spawnBoundaries.yBottomMax, (enemyData.spawnBoundaries.yTopMax + enemyData.spawnBoundaries.yBottomMax) / 2), 2);
        Vector3 rightPoint = new Vector3(Random.Range((enemyData.spawnBoundaries.xLeftMax + enemyData.spawnBoundaries.xRightMax) / 2, enemyData.spawnBoundaries.xLeftMax),
            Random.Range(enemyData.spawnBoundaries.yBottomMax, (enemyData.spawnBoundaries.yTopMax + enemyData.spawnBoundaries.yBottomMax) / 2), 2);
        Vector3 topPoint = new Vector3(Random.Range(enemyData.spawnBoundaries.xRightMax, enemyData.spawnBoundaries.xLeftMax),
            Random.Range((enemyData.spawnBoundaries.yTopMax + enemyData.spawnBoundaries.yBottomMax) / 2, enemyData.spawnBoundaries.yTopMax), 2);

        points = new()
        {
            topPoint,
            leftPoint,
            rightPoint
        };
        // Assign and activate the point objects
        for(int i = 0; i < 3; i++)
        {
            GameObject pointChild = gameObject.transform.GetChild(i).gameObject;
            Debug.Log(enemyData.health / 3);
            pointChild.GetComponent<GuardianPoint>().Heal(enemyData.health / 3);
            pointChild.transform.position = points[i];
            pointChild.SetActive(true);

            if(guardianPoints.Count < 3) guardianPoints.Add(pointChild);
        }
        points.Add(topPoint);
    }

    private void Update()
    {
        if(activePoints > 1)
        {
            for (int i = 0; i < activePoints; i++)
            {
                CastARay(guardianPoints[i].transform.position, guardianPoints[(i + 1) % activePoints].transform.position);
            }
        }
    }

    private void CastARay(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 direction = endPoint - startPoint;

        // Perform a raycast
        RaycastHit hit;
        if (Physics.Raycast(startPoint, direction, out hit))
        {
            // Check if the ray hit an object
            if (hit.collider != null)
            {
                // You can now access the object that was hit
                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.CompareTag("Projectile") && objectHit.GetComponent<SimpleProjectile>().IsSourcePlayer())
                {
                    GameEventHandler.Instance.DestroyObject(objectHit);
                }
            }
        }
    }
}
