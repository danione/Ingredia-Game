using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProtectionNotificationObject
{
    [SerializeField] private TimerNotificationObject timerNotification;

    public void Setup()
    {
        GameEventHandler.Instance.ShieldDisabled += OnShieldDisabled;
        GameEventHandler.Instance.SentShieldStats += OnShieldStatsSent;
    }

    private void OnShieldStatsSent(float duration)
    {
        timerNotification.ChangePulse(duration);
    }

    private void OnShieldDisabled()
    {
        timerNotification.ForgetObject();
    }

    public Transform GenerateANewTimer(TimerUIManager manager)
    {
        return timerNotification.CreateANewTimer(manager);
    }

}
