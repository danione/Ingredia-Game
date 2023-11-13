using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverloadRitual : Ritual
{
    public OverloadRitual(RitualScriptableObject data) : base(data)
    {
    }

    protected override void CompleteAnEvent()
    {
        PlayerEventHandler.Instance.CompleteBenevolentRitual(this);
    }

    protected override IPotion GetReward()
    {
        return new OverloadElixir();
    }
}
