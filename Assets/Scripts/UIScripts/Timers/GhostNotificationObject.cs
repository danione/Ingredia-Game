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
    private StateSwapper swapper = new();

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
        swapper.SetValue(timerNotification, System.Math.Round(ghostPowerPool, 1).ToString());
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
            swapper.ChangeImages(timerNotification, isTransforming);
            currentGhostState = isTransforming;
        }
    }

    public Transform GenerateANewTimer(TimerUIManager manager)
    {
        return timerNotification.CreateANewTimer(manager);
    }
}
