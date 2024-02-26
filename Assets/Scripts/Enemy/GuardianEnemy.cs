using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GuardianEnemy : Enemy
{
    [SerializeField] private float timeToRegenerate;
    [SerializeField] private List<Transform> edges;

    private HashSet<GuardianPoint> guardianPoints = new();
    private List<Vector3> points;

    private int activePoints = 0;

    private void OnDestroyedObject(GuardianPoint point)
    {
        if (activePoints > 0 && guardianPoints.Contains(point))
        {
            point.gameObject.SetActive(false);
            activePoints--;
            if (activePoints == 0)
            {
                DestroyEnemy();
            }

        }      
    }

    public override void ResetEnemy()
    {
        Init();

    }

    private void Init()
    {
        SetupGuardianPoints();
        GameEventHandler.Instance.PointDestroyed += OnDestroyedObject;
    }

    public override void DestroyEnemy()
    {
        GameEventHandler.Instance.PointDestroyed -= OnDestroyedObject;
        guardianPoints.Clear();
        points.Clear();
        base.DestroyEnemy();
    }

    private void RestorePoints(GuardianPoint point)
    {
        if (guardianPoints.Contains(point))
        {
            point.gameObject.SetActive(true);
            point.gameObject.GetComponent<IUnitStats>().Heal(enemyData.health / guardianPoints.Count);
            activePoints++;
        }
    }

    void PositionEdge(Vector3 point1, Vector3 point2, Transform edge)
    {
        // Set the position of the edge to the midpoint between the two points
        Vector3 midpoint = (point1 + point2) / 2f;
        edge.position = midpoint;

        // Set the scale of the edge to match the distance between the two points
        float distance = Vector3.Distance(point1, point2);
        edge.localScale = new Vector3(distance, 0.1f, 1f);

        // Rotate the edge to align with the line between the two points
        Vector3 direction = point2 - point1;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        edge.rotation = Quaternion.Euler(0f, 0f, angle);
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
            GuardianPoint pointChild = gameObject.transform.GetChild(i).gameObject.GetComponent<GuardianPoint>();
            pointChild.GetComponent<GuardianPoint>().Heal(enemyData.health / 3);
            pointChild.transform.position = points[i];
            pointChild.gameObject.SetActive(true);
            activePoints++;
            guardianPoints.Add(pointChild);
        }

        for (int i = 0; i < activePoints; i++)
        {
            PositionEdge(points[i], points[(i + 1) % activePoints], edges[i]);
        }
    }
}
