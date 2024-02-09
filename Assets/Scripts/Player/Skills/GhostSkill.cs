using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSkill
{
    private GhostPotionData data;
    private float countdownTimer; // How long until the potion is destroyed
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

        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            if (isTransforming)
            {
                Physics.IgnoreLayerCollision(0, 7, true);
                // ChangeAlpha(0.5f);
            }
            else
            {
                Physics.IgnoreLayerCollision(0, 7, false);
                // ChangeAlpha(1f);
            }
            GameEventHandler.Instance.SendGhostCurrentTimers(countdownTimer);
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
        if (isActive)
        {
            countdownTimer += data.countDownTimer;
            GameEventHandler.Instance.SendGhostCurrentTimers(countdownTimer);
            return;
        }

        this.data = data;
        countdownTimer = data.countDownTimer;
        isActive = true;
        GameEventHandler.Instance.GhostActivate();
    }
}
