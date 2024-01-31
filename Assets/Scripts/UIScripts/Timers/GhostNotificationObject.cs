using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class GhostNotificationObject
{
    [SerializeField] private TimerNotificationObject timerNotification;
    private bool currentGhostState = false;
    private float ghostPowerPool;

    public void Setup()
    {
        GameEventHandler.Instance.GhostActivated += OnGhostActivated;
        GameEventHandler.Instance.GhostDeactivated += OnGhostDeactivated;
        GameEventHandler.Instance.SentGhostCurrentTimers += OnGhostSendCurrentTimers;
    }

    private void OnGhostSendCurrentTimers(float timer, float pool)
    {
        ghostPowerPool = pool;
        timerNotification.ChangePulse(timer);
        timerNotification.currentObject.GetChild(2).GetComponent<TextMeshProUGUI>().text = System.Math.Round(ghostPowerPool, 1).ToString();
    }

    private void ChangeImages(bool isTransforming)
    {
        if (timerNotification.currentObject == null) return;

        if (isTransforming)
        {
            timerNotification.currentObject.GetChild(0).GetComponent<Image>().sprite = timerNotification.timerDataObject.BorderSpriteEngaged;
            timerNotification.currentObject.GetChild(1).GetComponent<Image>().sprite = timerNotification.timerDataObject.IconSpriteEngaged;
            timerNotification.currentObject.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            timerNotification.currentObject.GetChild(0).GetComponent<Image>().sprite = timerNotification.timerDataObject.BorderSpriteIdle;
            timerNotification.currentObject.GetChild(1).GetComponent<Image>().sprite = timerNotification.timerDataObject.IconSpriteIdle;
            timerNotification.currentObject.GetChild(2).gameObject.SetActive(false);
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
