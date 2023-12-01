using UnityEngine;
[CreateAssetMenu(fileName = "New Overload Elixir", menuName = "Scriptable Objects/Potion/Overload Elixir")]
public class OverloadElixirData : PotionsData
{
    public float durationInSeconds;
    public float usageStrengthInSeconds;
    public override void UsePotion()
    {
        GameEventHandler.Instance.ActivateLaser(this);
    }
}
