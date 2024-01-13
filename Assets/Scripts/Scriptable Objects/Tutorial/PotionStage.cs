using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Potion Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Potion Stage")]

public class PotionStage : TutorialStage
{
    public override void InitiateStage()
    {
        InputEventHandler.instance.UsedPotion += TutorialManager.instance.OnPotionUse;
        PlayerInputHandler.permissions.canUsePotions = true;
    }

    public override void NextStage()
    {
        InputEventHandler.instance.UsedPotion -= TutorialManager.instance.OnPotionUse;
    }
}
