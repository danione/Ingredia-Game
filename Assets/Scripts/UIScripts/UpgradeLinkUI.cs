using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLinkUI : MonoBehaviour
{
    [SerializeField] private List<UpgradeTrigger> source;
    [SerializeField] private List<UpgradeTrigger> destination;
    private bool isAvailable = false;
    private bool hasBeenUpgraded = false;
    private Image thisImage;

    private void Start()
    {
        thisImage = GetComponent<Image>();
        CheckIfUpgraded(false, source);
        CheckIfUpgraded(true, destination);
        GameEventHandler.Instance.UpgradeTriggered += OnUpgradeTriggered;
    }

    private void OnDestroy()
    {
        GameEventHandler.Instance.UpgradeTriggered -= OnUpgradeTriggered;
    }

    private void OnUpgradeTriggered(UpgradeData data)
    {
        foreach (UpgradeTrigger trigger in source)
        {
            if(trigger.GetData() == data)
            {
                CheckIfUpgraded(false, source);
                return;
            }
        }

        foreach (UpgradeTrigger trigger in destination)
        {
            if(trigger.GetData() == data)
            {
                CheckIfUpgraded(true, source);
            }
        }
    }

    private void CheckIfUpgraded(bool checkingDestination, List<UpgradeTrigger> data)
    {
        if (hasBeenUpgraded) return;

        isAvailable = true;
        foreach (var upgrade in data)
        {
            if (!upgrade.hasBeenUpgraded())
            {
                isAvailable = false;
                break;
            }
        }
        if(checkingDestination && isAvailable)
        {
            thisImage.color = Color.green;
            hasBeenUpgraded = true;
        } else if (isAvailable)
        {
            thisImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            thisImage.color = new Color(.5f, .5f, .5f, .5f);
        }
    }

}
