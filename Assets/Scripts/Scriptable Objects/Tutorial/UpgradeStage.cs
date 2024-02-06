using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Upgrade Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Upgrade Stage")]
public class UpgradeStage : TutorialStage
{
    public override void InitiateStage()
    {
        PlayerInputHandler.permissions.canOpenUpgrades = true;
        PlayerInputHandler.permissions.canOpenMenus = true;
    }

    public override void NextStage()
    {
        GameManager.Instance.ResumeGame();
        PlayerEventHandler.Instance.UpgradesMenuClose();
    }

    public override void Reward()
    {
        PlayerInputHandler.permissions.canOpenUpgrades = true;
        PlayerInputHandler.permissions.canOpenMenus = true;
        PlayerInputHandler.permissions.canMove = false;
        GameEventHandler.Instance.TutorialClick();
    }
}
