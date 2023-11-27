using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Movement Upgrade", menuName = "Scriptable Objects/Upgrades/Movement Upgrade")]
public class MovementUpgrade : UpgradeData
{
    public int newMovementSpeed;

    public override void ApplyUpgrade(GameObject obj)
    {
        obj.GetComponent<PlayerMovement>().SetMovementSpeed(newMovementSpeed);
    }
}
