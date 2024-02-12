using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Scriptable Objects/Potion/Fireball")]
public class FireballPotionData : PotionsData
{
    public int ammoAmount;

    public override void UsePotion()
    {
        PlayerController.Instance.inventory.AddAmmo("Fireball",ammoAmount);
    }
}
