using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    private List<IUpgrade> upgrades = new List<IUpgrade>();

    private void Start()
    {
        upgrades.Add(new HealthUpgrade());
        upgrades.Add(new SpeedUpgrade());
        foreach (var upgrade in upgrades)
        {
            upgrade.Init();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            upgrades[1].Upgrade();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerController.Instance.inventory.AddGold(100);
        }
    }
}
