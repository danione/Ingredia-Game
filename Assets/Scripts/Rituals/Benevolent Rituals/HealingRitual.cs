using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingRitual : Ritual
{
    protected override IReward GetReward()
    {
        return new HealthReward(2);
    }

    protected override Dictionary<string, int> GetRitualStages()
    {
        Dictionary<string, int> ritualStages = new Dictionary<string, int>()
        {
            { "ashes", 5},
            { "water", 3}
           
        };

        return ritualStages;
    }
}
