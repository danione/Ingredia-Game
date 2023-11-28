using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnhancedMovementState : IState
{
    private Enemy currentUnit;
    private float enhancedMovementSpeed = 18f;
    private short direction;
    private bool moveRight;

    public BatEnhancedMovementState(Enemy enemy) { 
        currentUnit = enemy;
        moveRight = Random.Range(0,1) == 0;
        direction = moveRight ? (short)1 : (short)-1;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
        // Nothing
    }

    void IState.Update()
    {
        if (currentUnit == null) return;

        Vector3 unitPos = currentUnit.gameObject.transform.position;

        if(!moveRight && unitPos.x > currentUnit.Boundaries.xLeftMax)
        {
            direction = -1;
        }
        else
        {
            moveRight = true;

            if(unitPos.x < currentUnit.Boundaries.xRightMax)
            {
                direction = 1;
            }
            else { 
                moveRight =  false;
            }
        }
        MoveDirection(direction);
    }

    void MoveDirection(short direction)
    {
        currentUnit.gameObject.transform.Translate(Vector3.right * direction * enhancedMovementSpeed * Time.deltaTime);
    }
}
