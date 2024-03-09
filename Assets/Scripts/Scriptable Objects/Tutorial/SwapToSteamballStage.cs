using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Swap To Steamball Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Swap To Steamball Stage")]
public class SwapToSteamballStage : TutorialStage
{
    public override void InitiateStage()
    {
        GameEventHandler.Instance.SwappedProjectiles += TutorialManager.instance.OnSwappedProjectiles;
        TutorialManager.instance.EnableSwapUI();
        PlayerController.Instance.inventory.AddAmmo("Steamball", 5);
    }

    public override void NextStage()
    {
        GameEventHandler.Instance.SwappedProjectiles -= TutorialManager.instance.OnSwappedProjectiles;
    }

    public override void Reward()
    {
        TutorialManager.instance.EnableSwapUI();
        PlayerController.Instance.inventory.AddAmmo("Steamball", 5);
    }
}
