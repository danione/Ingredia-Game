using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Cauldron Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Cauldron Stage")]
public class CauldronStage : TutorialStage
{
    public override void InitiateStage()
    {
        PlayerEventHandler.Instance.EmptiedCauldron += TutorialManager.instance.OnEmptiedCauldron;
        PlayerInputHandler.permissions.canEmptyCauldron = true;
    }

    public override void NextStage()
    {
        PlayerEventHandler.Instance.EmptiedCauldron -= TutorialManager.instance.OnEmptiedCauldron;
    }
}
