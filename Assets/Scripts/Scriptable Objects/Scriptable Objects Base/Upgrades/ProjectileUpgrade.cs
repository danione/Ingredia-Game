using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Upgrade", menuName = "Scriptable Objects/Upgrades/Projectile Upgrade")]
public class ProjectileUpgrade : UpgradeData
{
    public int damageAmount;
    public override void ApplyUpgrade(GameObject obj)
    {
        obj.GetComponent<SimpleProjectile>().ChangeStrength(damageAmount);
    }
}
