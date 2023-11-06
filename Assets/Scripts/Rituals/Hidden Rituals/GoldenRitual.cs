using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenRitual : Ritual
{
    public GoldenRitual(RitualScriptableObject data) : base(data)
    {
    }

    protected override void CompleteAnEvent()
    {
        PlayerEventHandler.Instance.CompleteBenevolentRitual(this);
    }

    protected override IPotion GetReward()
    {
        return null;
    }
}
