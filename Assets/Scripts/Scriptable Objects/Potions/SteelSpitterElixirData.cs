using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Steel Spitter Potion", menuName = "Scriptable Objects/Potion/Steel Spitter Potion")]
public class SteelSpitterElixirData : PotionsData
{
    public int amount;

    public override void UsePotion()
    {
        PlayerController.Instance.inventory.AddAmmo("Athame", amount);
    }
}
