using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverloadSkill
{
    private OverloadElixirData data;
    private float timer;
    private float strength;

    private LaserBeam laser;
    private bool isFiringLaser = false;
    private bool isActive = false;

    public OverloadSkill()
    {
        laser = new LaserBeam(PlayerController.Instance.transform, Vector3.up);
        PlayerEventHandler.Instance.LaserFired += OnFiringLaser;
    }

    private void OnFiringLaser(bool isPlayerPressingFiring)
    {
        isFiringLaser = isPlayerPressingFiring;
    }

    public void Tick()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;
        if (timer <= 0 || strength <= 0)
        {
            laser.Refresh();
            isActive = false;
            GameEventHandler.Instance.LaserDeactivate();
            return;
        }
        else if (isFiringLaser)
        {
            strength -= Time.deltaTime;
            laser.Execute();
        }
        else
        {
            laser.Refresh();
        }
        GameEventHandler.Instance.SendLaserStats(timer, strength);
    }

    public void CreateOrRefresh(OverloadElixirData data)
    {
        if (isActive)
        {
            timer += data.durationInSeconds;
            strength += data.durationInSeconds;
            return;
        }

        this.data = data;
        timer = data.durationInSeconds;
        strength = data.usageStrengthInSeconds;
        isActive = true;
        GameEventHandler.Instance.LaserActivate();
        // Some basic animation
    }
}
