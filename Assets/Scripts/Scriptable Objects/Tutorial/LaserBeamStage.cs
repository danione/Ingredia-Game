using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Laser Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Laser Stage")]
public class LaserBeamStage : TutorialStage
{
    public override void InitiateStage()
    {
        TutorialManager.instance.AddLaserBeam();
        GameEventHandler.Instance.DestroyedEnemy += TutorialManager.instance.OnWallDestroyed;
        
    }

    public override void NextStage()
    {
        GameEventHandler.Instance.LaserDeactivate();
        GameEventHandler.Instance.DestroyedEnemy -= TutorialManager.instance.OnWallDestroyed;
    }

    public override void Reward()
    {
        PlayerController.Instance.inventory.AddGold(30);
    }
}
