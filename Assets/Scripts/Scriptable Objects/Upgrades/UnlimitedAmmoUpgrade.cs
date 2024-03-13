using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Unlimited Ammo Upgrade", menuName = "Scriptable Objects/Upgrades/Unlimited Ammo")]

public class UnlimitedAmmoUpgrade : UpgradeData
{
    public string weaponName;
    public RitualScriptableObject ammoRitual;

    public override void ApplyUpgrade(GameObject obj)
    {
        PlayerController.Instance.GetComponent<PlayerInventory>().SetUnlimitedWeapon(weaponName);
        GameManager.Instance.GetComponent<IngredientManager>().RemoveRitualValues(ammoRitual);
        GameEventHandler.Instance.UpdateUI();
    }
}
