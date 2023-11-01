using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BansheeAttackState : AttackState
{
    private const float defaultChannel = 3.0f;
    private float channelValue;
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

    public override void Attack()
    {
        channelValue -= Time.deltaTime;
        if(channelValue < 0)
        {
            InputEventHandler.instance.SetMovement(isMoving: false);
        }
    }
}
