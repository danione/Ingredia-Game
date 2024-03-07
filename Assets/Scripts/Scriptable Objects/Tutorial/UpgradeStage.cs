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
        PlayerEventHandler.Instance.StopLaser();
    }

    public override void NextStage()
    {
        GameManager.Instance.ResumeGame();
        PlayerEventHandler.Instance.UpgradesMenuOpen();
    }

    public override void Reward()
    {
        PlayerEventHandler.Instance.StopLaser();
        PlayerInputHandler.permissions.canOpenUpgrades = true;
        PlayerInputHandler.permissions.canOpenMenus = true;
        PlayerInputHandler.permissions.canMove = false;
        GameEventHandler.Instance.TutorialClick();
    }
}
