using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Laser Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Laser Prep Stage")]

public class PrepForLaserStage : TutorialStage
{
    public RectTransform LaserPos;
    public override void InitiateStage()
    {
        TutorialManager.instance.LaserPreparation();
        TutorialManager.instance.DisableIngredientsFactory();
    }

    public override void NextStage()
    {
       PlayerInputHandler.permissions.canMove = false;
    }

    public override void Reward()
    {
        PlayerInputHandler.permissions.canMove = false;
        PlayerController.Instance.gameObject.transform.position = LaserPos.position;
    }
}
