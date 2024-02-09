using System;
using UnityEngine;
[System.Serializable]
public class NotificationObject
{
    [SerializeField] private TimerNotificationObject timerNotification;
    private StateSwapper swapper = new();
    private bool currentState = false;
    private int currentPos;
    private TimerUIManager manager;

    public void SetupManager(TimerUIManager assignedManager)
    {
        manager = assignedManager;
    }

    public int GetCurrentPos()
    {
        return currentPos;
    }

    public void OnSendCurrentTimers(float timer)
    {
        timerNotification.ChangePulse(timer);
        swapper.SetValue(timerNotification, Math.Round(timer, 1).ToString());
    }

    public void OnDeactivated()
    {
        timerNotification.ForgetObject();
        manager.OnAnyDisabled(currentPos);
    }

    public void OnTransform(bool isTransforming)
    {
        if (currentState != isTransforming)
        {
            swapper.ChangeImages(timerNotification, isTransforming);
            currentState = isTransforming;
        }
    }

    public Transform GenerateANewTimer(TimerUIManager manager, int currentId)
    {
        if (timerNotification.currentObject != null) return null;

        currentPos = currentId;
        return timerNotification.CreateANewTimer(manager);
    }

    public void ChangePos(int newPos, float offsetY)
    {
        currentPos = newPos;
        timerNotification.ChangePos(offsetY);
    }
}
