using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BansheeAttackState : AttackState
{
    private const float defaultChannel = 3.0f;
    private float channelValue;
    private float pickDirectionChange;
    private bool hasPickedRandomDirection = false;
    public Action MovementTampering;

    public BansheeAttackState(PlayerController controller, Enemy currentUnit) : base(controller, currentUnit)
    {
        channelValue = defaultChannel;
    }

    public override void Enter()
    {
        base.Enter();
        channelValue = defaultChannel;
    }

    public override void Exit()
    {
        base.Exit();
        InputEventHandler.instance.SetMovement(isMoving: true);
    }

    private void MoveCharacter()
    {
        DangerousObject closestDObject = FindClosestDangerousObject();
        if(closestDObject == null)
        {
            InputEventHandler.instance.MovePlayerRandomly();
        }
        else
        {
            Vector3 directionToObject =  controller.transform.position - closestDObject.transform.position;
            float directionToThisObject = directionToObject.normalized.x;
            InputEventHandler.instance.MoveTowards(directionToThisObject);
        }
    }

    private DangerousObject FindClosestDangerousObject()
    {
        DangerousObject[] closestDObjects = GameObject.FindObjectsOfType<DangerousObject>();
        DangerousObject closestDObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (DangerousObject dObject in closestDObjects)
        {
            Vector3 directionToEnemy = dObject.transform.position - dObject.transform.position;
            float directionToThisEnemy = directionToEnemy.magnitude;
            if (directionToThisEnemy < closestDistance)
            {
                closestDistance = directionToThisEnemy;
                closestDObject = dObject;
            }
        }

        return closestDObject;
    }

    public override void Attack()
    {
        channelValue -= Time.deltaTime;
        if(channelValue < 0)
        {
            InputEventHandler.instance.SetMovement(isMoving: false);
            if(hasPickedRandomDirection == false)
            {
                InputEventHandler.instance.PickRandomDirection();
                hasPickedRandomDirection = true;
                pickDirectionChange = UnityEngine.Random.Range(1f, 2.5f);
            }

            MoveCharacter();

            pickDirectionChange -= Time.deltaTime;
            if(pickDirectionChange < 0)
            {
                hasPickedRandomDirection = false;
            }
        }
    }
}
