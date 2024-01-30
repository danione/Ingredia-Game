using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSkill
{
    private GhostPotionData data;
    private float countdownTimer; // How long until the potion is destroyed
    private float powerPool; // How much the entity can be untargetable for
    private bool isTransforming = false;
    private bool isActive = false;

    public GhostSkill()
    {
        PlayerEventHandler.Instance.TransformIntoGhost += OnGhostTransform;
    }

    private void OnGhostTransform(bool isTargetTransforming)
    {
        isTransforming = isTargetTransforming;
    }

    public void Tick()
    {
        if (!isActive) return;

        if (countdownTimer > 0 && powerPool > 0)
        {
            countdownTimer -= Time.deltaTime;
            if (isTransforming)
            {
                powerPool -= Time.deltaTime;
                Physics.IgnoreLayerCollision(0, 7, true);
                // ChangeAlpha(0.5f);
            }
            else
            {
                Physics.IgnoreLayerCollision(0, 7, false);
                // ChangeAlpha(1f);
            }
        }
        else
        {
            Physics.IgnoreLayerCollision(0, 7, false);
            isActive = false;
            GameEventHandler.Instance.GhostDeactivated();
        }
    }

    public void CreateOrReset(GhostPotionData data)
    {
        // Visual transparency
        this.data = data;
        countdownTimer = data.countDownTimer;
        powerPool = data.powerPoolValue;
        isActive = true;
        GameEventHandler.Instance.GhostActivate();
    }
}
