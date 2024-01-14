using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Scroll Slip Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Scroll Slip Stage")]
public class ScrollSlipStage : TutorialStage
{
    public override void InitiateStage()
    {
        PlayerInputHandler.permissions.canOpenScrollMenu = true;
        PlayerEventHandler.Instance.OpenedScrollsMenu += TutorialManager.instance.OnScrollMenuOpened;
        TutorialManager.instance.ScrollGenerate();
    }

    public override void NextStage()
    {
        PlayerEventHandler.Instance.OpenedScrollsMenu -= TutorialManager.instance.OnScrollMenuOpened;
    }
}
