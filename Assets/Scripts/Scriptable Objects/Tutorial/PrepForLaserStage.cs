using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Laser Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Laser Prep Stage")]

public class PrepForLaserStage : TutorialStage
{
    public override void InitiateStage()
    {
        TutorialManager.instance.LaserPreparation();
    }

    public override void NextStage()
    {
       PlayerInputHandler.permissions.canMove = false;
    }
}
