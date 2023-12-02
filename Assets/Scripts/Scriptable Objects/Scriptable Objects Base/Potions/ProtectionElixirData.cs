using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Protection Elixir", menuName = "Scriptable Objects/Potion/Protection Elixir")]
public class ProtectionElixirData : PotionsData
{
    public float durationInSeconds;
    public override void UsePotion()
    {
        GameEventHandler.Instance.ActivateBarrier(durationInSeconds);
    }
}
