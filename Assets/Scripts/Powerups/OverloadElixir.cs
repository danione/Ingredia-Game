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
    
    public void Destroy()
    {
        destroyed = true;
        laser.Refresh();
        if (destroyed)
        {
            Debug.Log("Here too!");
        }
    }

    public void Tick()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 || strength <= 0)
        {
            Destroy();
            return;
        }else if (Input.GetKey(KeyCode.Q))
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
        // Some basic animation
    }
}
