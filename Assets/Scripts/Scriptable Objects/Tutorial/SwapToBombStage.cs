using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Swap To Bomb Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Swap To Bomb Stage")]
public class SwapToBombStage : TutorialStage
{
    public override void InitiateStage()
    {
        GameEventHandler.Instance.SwappedProjectiles += TutorialManager.instance.OnSwappedProjectiles;
        PlayerController.Instance.inventory.AddFlameBombAmmo(5);
    }

    public override void NextStage()
    {
        GameEventHandler.Instance.SwappedProjectiles -= TutorialManager.instance.OnSwappedProjectiles;
    }
}
