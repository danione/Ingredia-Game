using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverloadElixir : IPotion
{
    private const float timerDefault = 30.0f;
    private const float strengthDefault = 20.0f;
    private float timer;
    private float strength;
    private bool destroyed = false;
    public bool Destroyed => destroyed;
    private LaserBeam laser;
    private bool isFiringLaser = false;
    
    public void Destroy()
    {
        destroyed = true;
        laser.Refresh();
        PlayerEventHandler.Instance.LaserFired -= OnFiringLaser;
    }

    public void OnFiringLaser(bool isPlayerPressingFiring)
    {
        isFiringLaser = isPlayerPressingFiring;
    }

    public void Tick()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 || strength <= 0)
        {
            Destroy();
            return;
        }else if (isFiringLaser)
        {
            strength -= Time.deltaTime;
            laser.Execute();
        }
        else
        {
            laser.Refresh();
        }
    }

    public void Use()
    {
        laser = new LaserBeam(PlayerController.Instance.transform, Vector3.up);
        PlayerEventHandler.Instance.LaserFired += OnFiringLaser;
        timer = timerDefault;
        strength = strengthDefault;
        // Some basic animation
    }

    public void Reset()
    {
        destroyed = false;
    }
}
