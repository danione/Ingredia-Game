using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverloadElixir : MonoBehaviour, IPowerUp
{
    private float timer = 10.0f;
    private float strength = 3.20f;
    private bool destroyed = false;
    public bool Destroyed => destroyed;
    private LaserBeam laser;
    private bool isFiringLaser = false;
    
    public void Destroy()
    {
        destroyed = true;
        laser.Refresh();
        if (destroyed)
        {
            Debug.Log("Here too!");
        }
        PlayerEventHandler.Instance.LaserFired -= OnFiringLaser;
    }

    public void OnFiringLaser()
    {
        isFiringLaser = true;
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
            isFiringLaser = false;
        }
    }

    public void Use()
    {
        laser = new LaserBeam(PlayerController.Instance.transform, Vector3.up);
        PlayerEventHandler.Instance.LaserFired += OnFiringLaser;
        // Some basic animation
    }
}
