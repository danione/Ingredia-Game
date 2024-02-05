using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Movement Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Movement Stage")]
public class MovementStage : TutorialStage
{
    public override void InitiateStage()
    {
        PlayerInputHandler.permissions.LockAll();
        InputEventHandler.instance.PlayerMoved += TutorialManager.instance.OnPlayerMoved;
    }

    public override void NextStage()
    {
        PlayerInputHandler.permissions.canMove = true;
        InputEventHandler.instance.PlayerMoved -= TutorialManager.instance.OnPlayerMoved;
    }

    public override void Reward()
    {
        PlayerInputHandler.permissions.canMove = true;
    }
}
