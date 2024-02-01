using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
[System.Serializable]
public class NotificationObject
{
    private float powerPool;
    [SerializeField] private TimerNotificationObject timerNotification;
    private StateSwapper swapper = new();
    private bool currentState = false;
    private int currentPos;
    private static TimerUIManager manager;

    public static void SetupManager(TimerUIManager assignedManager)
    {
        manager = assignedManager;
    }

    public int GetCurrentPos()
    {
        return currentPos;
    }

    public void OnSendCurrentTimers(float timer, float pool)
    {
        powerPool = pool;
        timerNotification.ChangePulse(timer);
        swapper.SetValue(timerNotification, Math.Round(powerPool, 1).ToString());
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
        currentPos = currentId;
        return timerNotification.CreateANewTimer(manager);
    }

    public void ChangePos(int newPos, float offsetY)
    {
        currentPos = newPos;
        timerNotification.ChangePos(offsetY);
    }
}
