using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostNotificationObject : TimerNotificationObject
{
    private bool currentGhostState = false;
    private bool active;

    protected override void FurtherSetupInstructions()
    {
        GameEventHandler.Instance.GhostActivated += OnGhostActivated;
        GameEventHandler.Instance.GhostDeactivated += OnGhostDeactivated;
        active = true;
    }

    private void ChangeImages(bool isTransforming)
    {
        if (currentObject == null) return;

        if (isTransforming)
        {
            currentObject.GetChild(0).GetComponent<Image>().sprite = timerDataObject.BorderSpriteEngaged;
            currentObject.GetChild(1).GetComponent<Image>().sprite = timerDataObject.IconSpriteEngaged;
        }
        else
        {
            currentObject.GetChild(0).GetComponent<Image>().sprite = timerDataObject.BorderSpriteIdle;
            currentObject.GetChild(1).GetComponent<Image>().sprite = timerDataObject.IconSpriteIdle;
        }
    }

    private void OnGhostActivated()
    {
        PlayerEventHandler.Instance.TransformIntoGhost += OnTransformIntoGhost;
        active = true;
        // StartCoroutine(Pulse());
    }

    private void OnGhostDeactivated()
    {
        PlayerEventHandler.Instance.TransformIntoGhost -= OnTransformIntoGhost;
        active = false;
    }

    private void OnTransformIntoGhost(bool isTransforming)
    {
        if (currentGhostState != isTransforming)
        {
            ChangeImages(isTransforming);
            currentGhostState = isTransforming;
        }
    }
}
