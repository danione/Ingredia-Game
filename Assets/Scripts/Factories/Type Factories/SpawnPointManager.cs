using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointManager
{
    private List<SpawnPoint> xPoints = new();
    private List<SpawnPoint> yPoints = new();

    private Queue<SpawnPoint> spawnPointsXOnCooldown = new();
    private Queue<SpawnPoint> spawnPointsYOnCooldown = new();

    private float dequeueCooldown;

    public SpawnPointManager(BoundariesData spawnLocation, float dequeueCooldown)
    {
        // Initialise the spawn points
        int numberOfXPoints = CountPointsBetween(spawnLocation.xLeftMax, spawnLocation.xRightMax, spawnLocation.offsetX);
        int numberOfYPoints = CountPointsBetween(spawnLocation.yBottomMax, spawnLocation.yTopMax, spawnLocation.offsetY);

        // Fill them in
        xPoints = Enumerable.Range((int)spawnLocation.xLeftMax, numberOfXPoints - 1).Select(x => new SpawnPoint(x + spawnLocation.offsetX)).ToList();
        if(spawnLocation.offsetY == 0)
        {
            yPoints.Add(new SpawnPoint(spawnLocation.yTopMax));
        }
        else
        {
            yPoints = Enumerable.Range((int)spawnLocation.yTopMax, numberOfYPoints - 1).Select(y => new SpawnPoint(y - spawnLocation.offsetY)).ToList();
        }

        // How quick the points reset
        this.dequeueCooldown = dequeueCooldown;
    }

    private int CountPointsBetween(float leftMax, float rightMax, float offset)
    {
        // Calculate the distance between the x-coordinates of the start and end points
        float distance = Mathf.Abs(rightMax - leftMax);
        // Calculate the number of points that can fit along the x-axis between the two points
        int numberOfPoints = Mathf.FloorToInt(distance / offset);
        return numberOfPoints;
    }


    public SpawnPoint PickNewRandomPoint(bool isXPoint)
    {
        SpawnPoint newXPoint = null;
        List<SpawnPoint> pointList = isXPoint ? xPoints : yPoints;
        Queue <SpawnPoint> queueList = isXPoint ? spawnPointsXOnCooldown : spawnPointsYOnCooldown;
        
        if (pointList.Count == 1) return pointList[0];


        newXPoint = pointList.Where(p => !p.isInQueue).OrderBy(p => Guid.NewGuid()).FirstOrDefault();

        if (newXPoint == null) return null;

        queueList.Enqueue(newXPoint);
        newXPoint.isInQueue = true;
        return newXPoint;
    }

    public IEnumerator ResetNextPoint()
    {
        while (!GameManager.Instance.gameOver)
        {
            if (spawnPointsXOnCooldown.Count > 0)
            {
                SpawnPoint point = spawnPointsXOnCooldown.Dequeue();
                point.isInQueue = false;
            }
            if(spawnPointsYOnCooldown.Count > 0)
            {
                SpawnPoint point = spawnPointsXOnCooldown.Dequeue();
                point.isInQueue = false;
            }
            yield return new WaitForSeconds(dequeueCooldown);
        }
    }
}
