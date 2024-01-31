using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class GhostNotificationObject
{
    [SerializeField] private TimerNotificationObject timerNotification;
    private bool currentGhostState = false;
    public void Setup()
    {
        GameEventHandler.Instance.GhostActivated += OnGhostActivated;
        GameEventHandler.Instance.GhostDeactivated += OnGhostDeactivated;
    }

    private void ChangeImages(bool isTransforming)
    {
        if (timerNotification.currentObject == null) return;

        if (isTransforming)
        {
            timerNotification.currentObject.GetChild(0).GetComponent<Image>().sprite = timerNotification.timerDataObject.BorderSpriteEngaged;
            timerNotification.currentObject.GetChild(1).GetComponent<Image>().sprite = timerNotification.timerDataObject.IconSpriteEngaged;
        }
        else
        {
            timerNotification.currentObject.GetChild(0).GetComponent<Image>().sprite = timerNotification.timerDataObject.BorderSpriteIdle;
            timerNotification.currentObject.GetChild(1).GetComponent<Image>().sprite = timerNotification.timerDataObject.IconSpriteIdle;
        }
    }

    private void OnGhostActivated()
    {
        PlayerEventHandler.Instance.TransformIntoGhost += OnTransformIntoGhost;
    }

    private void OnGhostDeactivated()
    {
        PlayerEventHandler.Instance.TransformIntoGhost -= OnTransformIntoGhost;
        timerNotification.ForgetObject();
    }

    private void OnTransformIntoGhost(bool isTransforming)
    {
        if (currentGhostState != isTransforming)
        {
            ChangeImages(isTransforming);
            currentGhostState = isTransforming;
        }
    }

    public Transform GenerateANewTimer(TimerUIManager manager)
    {
        return timerNotification.CreateANewTimer(manager);
    }
}
