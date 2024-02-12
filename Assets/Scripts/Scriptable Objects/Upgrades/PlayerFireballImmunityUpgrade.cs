using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Player Fireball Immunity Upgrade", menuName = "Scriptable Objects/Upgrades/Player Fireball Immunity")]
public class PlayerFireballImmunityUpgrade : UpgradeData
{
    public override void ApplyUpgrade(GameObject obj)
    {
        if (obj == null) return;
        obj.GetComponent<FireballProjectile>().StopAffectingPlayer();
    }
}
