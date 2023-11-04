using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingRitual : Ritual
{
    protected override void CompleteAnEvent()
    {
        PlayerEventHandler.Instance.CompleteBenevolentRitual(this);
    }

    protected override IReward GetReward()
    {
        return new HealthReward(2);
    }

    protected override void SetUpScriptableObject()
    {
        SriptableObject = = NewRitual.CreateInstance<NewRitual>();
    }
}
