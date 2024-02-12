using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Ritual Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Ritual Stage")]
public class RitualStage : TutorialStage
{
    public PotionsData athameThrowPotion;

    public override void InitiateStage()
    {
        PlayerEventHandler.Instance.PerformedRitual += TutorialManager.instance.OnPerformedFirstRitual;
        PlayerInputHandler.permissions.canPerformRituals = true;
        GameManager.Instance.gameObject.GetComponent<RitualManager>().enabled = true;
        TutorialManager.instance.FirstRitual();
    }

    public override void NextStage()
    {
        PlayerEventHandler.Instance.PerformedRitual -= TutorialManager.instance.OnPerformedFirstRitual;

    }

    public override void Reward()
    {
        PlayerInputHandler.permissions.canPerformRituals = true;
        GameManager.Instance.gameObject.GetComponent<RitualManager>().enabled = true;
        TutorialManager.instance.FirstRitual();
        PlayerController.Instance.inventory.AddPotion(athameThrowPotion);
    }
}
