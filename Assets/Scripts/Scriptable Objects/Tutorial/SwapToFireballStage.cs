using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Swap To Fireball Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Swap To Fireball Stage")]
public class SwapToFireballStage : TutorialStage
{
    public override void InitiateStage()
    {
        GameEventHandler.Instance.SwappedProjectiles += TutorialManager.instance.OnSwappedProjectiles;
        TutorialManager.instance.EnableSwapUI();
        PlayerController.Instance.inventory.AddAmmo("Fireball", 5);
    }

    public override void NextStage()
    {
        GameEventHandler.Instance.SwappedProjectiles -= TutorialManager.instance.OnSwappedProjectiles;
    }

    public override void Reward()
    {
        TutorialManager.instance.EnableSwapUI();
        PlayerController.Instance.inventory.AddAmmo("Fireball", 5);
    }
}
