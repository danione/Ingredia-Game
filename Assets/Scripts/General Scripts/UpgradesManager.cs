using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] public List<UIUpgradeClass> rightSideUpgrades = new();
    [SerializeField] public List<UIUpgradeClass> availableUpgrades = new();
}
