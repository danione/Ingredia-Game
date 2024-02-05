using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Movement Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Movement Stage")]
public class MovementStage : TutorialStage
{
    public override void InitiateStage()
    {
        try
        {
            Debug.Log("Locking permissions...");
            PlayerInputHandler.permissions.LockAll();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        try
        {
            Debug.Log("Moving permissions subscribing...");
            InputEventHandler.instance.PlayerMoved += TutorialManager.instance.OnPlayerMoved;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public override void NextStage()
    {
        PlayerInputHandler.permissions.canMove = true;
        InputEventHandler.instance.PlayerMoved -= TutorialManager.instance.OnPlayerMoved;
    }
}
