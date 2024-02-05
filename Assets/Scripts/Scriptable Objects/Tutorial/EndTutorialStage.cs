using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tutorial End Stage", menuName = "Scriptable Objects/Tutorials/Tutorial End Stage")]
public class EndTutorialStage : TutorialStage
{
    public override void InitiateStage()
    {
        TutorialManager.instance.TransitionOnly();
    }

    public override void NextStage()
    {
        PlayerInputHandler.permissions.canMove = true;
        TutorialManager.instance.ExitTutorial();
    }

    public override void Reward()
    {
        PlayerInputHandler.permissions.canMove = true;
        TutorialManager.instance.ExitTutorial();
    }
}
