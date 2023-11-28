using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fireball Area Upgrade", menuName = "Scriptable Objects/Upgrades/Fireball Area Upgrade")]
public class FireballAreaUpgrade : UpgradeData
{
    public float areaIncreaseAmount;

    public override void ApplyUpgrade(GameObject obj)
    {
        obj.GetComponent<FireBombProjectile>().SetAreaOfEffect(areaIncreaseAmount);
    }
}
