using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuperBatAttackState : IState
{
    private BatEnemy currentUnit;
    private Timer timer;
    private float defaultTeleportationCooldown = 2f;
    private List<float> availableTeleportationXCoordinates = new();
    private const int defaultNumberOfCoordinates = 3;
    private int coordinatesVisited;
    private int indexOfLastLocation;
    private float incrementalStep;
    private static readonly System.Random random = new System.Random();

    public SuperBatAttackState(BatEnemy enemy)
    {
        currentUnit = enemy;
        incrementalStep = (enemy.Boundaries.xLeftMax - enemy.Boundaries.xRightMax) / defaultNumberOfCoordinates;
        timer = new Timer(defaultTeleportationCooldown);
    }

    public void Enter()
    {
        Timer timer = new Timer(defaultTeleportationCooldown);
        coordinatesVisited = defaultNumberOfCoordinates;
        indexOfLastLocation = -1;
        availableTeleportationXCoordinates = Enumerable.Range(0, defaultNumberOfCoordinates)
                                            .Select(i => currentUnit.Boundaries.xRightMax + i * incrementalStep + (float)(random.NextDouble() * incrementalStep))
                                            .ToList();
        Teleport();
    }

    public void Exit()
    {
        //
    }

    public void Update()
    {
        if(coordinatesVisited != 0)
        {
            if (timer.IsFinished())
            {
                timer.Restart();
                Teleport();
            }
        }
    }

    private void Teleport()
    {
        int indexOfNextLocation = UnityEngine.Random.Range(0, availableTeleportationXCoordinates.Count);
        if(indexOfNextLocation != indexOfLastLocation)
        {
            Vector3 position = currentUnit.transform.position;
            currentUnit.transform.position = new Vector3(availableTeleportationXCoordinates[indexOfNextLocation], position.y, position.z);
            indexOfLastLocation = indexOfNextLocation;
        }
        coordinatesVisited--;
    }
}

public class Timer
{
    private float startTime;
    private float duration;

    public Timer(float duration)
    {
        this.duration = duration;
        Restart();
    }

    public void Restart()
    {
        startTime = Time.time;
    }

    public bool IsFinished()
    {
        return Time.time >= startTime + duration;
    }
}
