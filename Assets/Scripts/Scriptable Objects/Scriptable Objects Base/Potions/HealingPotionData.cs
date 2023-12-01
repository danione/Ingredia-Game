using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Potion", menuName = "Scriptable Objects/Potion/Healing Potion")]
public class HealingPotionData : PotionsData
{
    public int amount;

    public override void UsePotion()
    {
        PlayerController.Instance.stats.Heal(amount);
    }
}
