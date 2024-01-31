using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LaserNotificationObject
{
    [SerializeField] private TimerNotificationObject timerNotification;
    private StateSwapper swapper = new();
    private bool isFiringLaser = false;
    private float laserPowerPool;

    public void Setup()
    {
        GameEventHandler.Instance.LaserActivated += OnLaserActivate;
        GameEventHandler.Instance.LaserDeactivated += OnLaserDeactivate;
        GameEventHandler.Instance.SentLaserStats += OnSentLaserStats;

    }

    private void OnSentLaserStats(float timer, float strength)
    {
        laserPowerPool = strength;
        timerNotification.ChangePulse(timer);
        swapper.SetValue(timerNotification, System.Math.Round(laserPowerPool, 1).ToString());
    }

    private void OnFiringLaser(bool isFiring)
    {
        if(isFiringLaser != isFiring)
        {
            swapper.ChangeImages(timerNotification, isFiring);
            isFiringLaser = isFiring;
        }
    }

    private void OnLaserActivate()
    {
        PlayerEventHandler.Instance.LaserFired += OnFiringLaser;

    }

    private void OnLaserDeactivate()
    {
        PlayerEventHandler.Instance.LaserFired -= OnFiringLaser;
        timerNotification.ForgetObject();
    }

    public Transform GenerateANewTimer(TimerUIManager manager)
    {
        return timerNotification.CreateANewTimer(manager);
    }
}
