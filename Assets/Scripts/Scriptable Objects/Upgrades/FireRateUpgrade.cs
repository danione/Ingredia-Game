using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Fire Rate Upgrade", menuName = "Scriptable Objects/Upgrades/Fire Rate Upgrade")]
public class FireRateUpgrade : UpgradeData
{
    public float fireRate;
    public override void ApplyUpgrade(GameObject obj)
    {
        obj.GetComponent<PlayerInputHandler>().ChangeFireRate(fireRate);
    }
}
