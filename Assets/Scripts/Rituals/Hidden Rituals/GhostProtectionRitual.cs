using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostProtectionRitual : Ritual
{
    public GhostProtectionRitual(RitualScriptableObject data) : base(data)
    {
    }

    protected override void CompleteAnEvent()
    {
        PlayerEventHandler.Instance.CompleteBenevolentRitual(this);
    }

    protected override IPotion GetReward()
    {
        return new GhostPotion(PlayerController.Instance.gameObject.transform);
    }
}
