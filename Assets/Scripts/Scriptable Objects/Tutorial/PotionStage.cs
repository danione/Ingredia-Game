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
        TutorialManager.instance.EnableAmmoUI();
        InputEventHandler.instance.UsedPotion -= TutorialManager.instance.OnPotionUse;
    }

    public override void Reward()
    {
        TutorialManager.instance.EnableAmmoUI();
        PlayerInputHandler.permissions.canUsePotions = true;
    }
}
