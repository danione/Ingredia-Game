using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Golden Ticket Elixir", menuName = "Scriptable Objects/Potion/Golden Ticket Elixir")]
public class GoldenTicketElixirData : PotionsData
{
    public int nuggetValue;
    public List<string> objectTagsToConvert;

    public override void UsePotion()
    {
        GameEventHandler.Instance.ConvertingAllObjectsToGoldenNuggets(nuggetValue, objectTagsToConvert);
    }
}
