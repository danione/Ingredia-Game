using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Scriptable Objects/Potion/Fire Spitter")]
public class FireSpitterPotionData : PotionsData
{
    public int ammoAmount;

    public override void UsePotion()
    {
        PlayerController.Instance.inventory.AddAmmo("Bomb",ammoAmount);
    }
}
