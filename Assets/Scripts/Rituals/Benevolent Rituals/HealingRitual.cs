using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingRitual : Ritual
{
    public HealingRitual(RitualScriptableObject data) : base(data)
    {

    }

    protected override void CompleteAnEvent()
    {
        PlayerEventHandler.Instance.CompleteBenevolentRitual(this);
    }

    protected override IReward GetReward()
    {
        return new HealthReward(1);
    }
}
