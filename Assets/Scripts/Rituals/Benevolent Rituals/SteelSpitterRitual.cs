using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelSpitterRitual : Ritual
{
    public SteelSpitterRitual(RitualScriptableObject data) : base(data)
    {
    }

    protected override void CompleteAnEvent()
    {
        PlayerEventHandler.Instance.CompleteBenevolentRitual(this);
    }

    // To be changed
    protected override IPotion GetReward()
    {
        return new FireSpitterElixir();
    }
}
