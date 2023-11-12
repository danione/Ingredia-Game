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
        PlayerEventHandler.Instance.UnlockThisRitual();
    }

    protected override IPotion GetReward()
    {
        return new GoldenTicketElixir(GameObjectsHolder.instance.GoldenNuggets.transform);
    }
}
