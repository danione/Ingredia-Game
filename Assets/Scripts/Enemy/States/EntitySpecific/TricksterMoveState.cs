using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TricksterMoveState : IState
{
    private EnemyData _data;
    private Vector3 endOfMovement;
    private TricksterEnemy thisUnit;
    private float movementSpeed;
    private float stoppingDistance = 0.3f;

    public TricksterMoveState(EnemyData _data, TricksterEnemy thisUnit)
    {
        this._data = _data;
        this.thisUnit = thisUnit;
        this.movementSpeed = this._data.movementSpeed;
    }

    public void Enter()
    {
        int randomPosXorY = EitherOr(0, 1);
        if(randomPosXorY == 0)
        {
            endOfMovement = new Vector3(EitherOr(_data.spawnBoundaries.xLeftMax, _data.spawnBoundaries.xRightMax), 
                Random.Range(_data.spawnBoundaries.yTopMax, _data.spawnBoundaries.yBottomMax),2);
        }
        else
        {
            endOfMovement = new Vector3(Random.Range(_data.spawnBoundaries.xLeftMax, _data.spawnBoundaries.xRightMax), 
                EitherOr(_data.spawnBoundaries.yTopMax, _data.spawnBoundaries.yBottomMax), 2);
        }
    }

    public void Exit()
    {

    }

    public void Update()
    {
        Vector3 direction = (endOfMovement - thisUnit.transform.position).normalized;

        // Move towards the target
        thisUnit.transform.position += direction * movementSpeed * Time.deltaTime;

        if (Vector3.Distance(thisUnit.transform.position, endOfMovement) <= stoppingDistance)
            Enter();
    }

    public float EitherOr(float a, float b)
    {
        int randomRange = Random.Range(0, 2);
        return randomRange == 0 ? a : b;
    }

    public int EitherOr(int a, int b)
    {
        int randomRange = Random.Range(0, 2);
        return randomRange == 0 ? a : b;
    }
}
