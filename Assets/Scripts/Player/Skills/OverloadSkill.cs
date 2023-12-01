using System.Collections;
using System.Collections.Generic;
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
    }

    public void CreateOrRefresh(OverloadElixirData data)
    {
        this.data = data;
        timer = data.durationInSeconds;
        strength = data.usageStrengthInSeconds;
        isActive = true;
        // Some basic animation
    }
}
