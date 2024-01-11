using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Player Bomb Immunity Upgrade", menuName = "Scriptable Objects/Upgrades/Player Bomb Immunity")]
public class PlayerBombImmunityUpgrade : UpgradeData
{
    public override void ApplyUpgrade(GameObject obj)
    {
        if (obj == null) return;
        obj.GetComponent<FireBombProjectile>().StopAffectingPlayer();
    }
}
