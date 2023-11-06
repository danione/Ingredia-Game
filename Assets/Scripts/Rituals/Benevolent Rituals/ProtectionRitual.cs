using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionRitual : Ritual
{
    public ProtectionRitual(RitualScriptableObject data) : base(data)
    {
    }

    protected override void CompleteAnEvent()
    {
        PlayerEventHandler.Instance.CompleteBenevolentRitual(this);
    }

    protected override IPotion GetReward()
    {
        return new ProtectionElixir();
    }
}
