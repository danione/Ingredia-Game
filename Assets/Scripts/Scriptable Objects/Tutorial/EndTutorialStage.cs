using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tutorial End Stage", menuName = "Scriptable Objects/Tutorials/Tutorial End Stage")]
public class EndTutorialStage : TutorialStage
{
    public override void InitiateStage()
    {
        TutorialManager.instance.StartTransition();
    }

    public override void NextStage()
    {
        TutorialManager.instance.ExitTutorial();
    }
}
