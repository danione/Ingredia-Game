using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Scriptable Objects/Potion/Steamball")]
public class SteamballPotionsData : PotionsData
{
    public int ammoAmount;

    public override void UsePotion()
    {
        PlayerController.Instance.inventory.AddAmmo("Steamball",ammoAmount);
    }
}
