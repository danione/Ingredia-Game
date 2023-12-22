using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
   // [SerializeField] private int currentStage = 1;
    [SerializeField] private float playerMovementThreshold;


    private void Start()
    {
        InputEventHandler.instance.PlayerMoved += OnPlayerMoved;
    }

    private void OnPlayerMoved(float direction)
    {
        playerMovementThreshold -= Time.deltaTime;
        if(playerMovementThreshold < 0)
        {
            PlayerInputHandler.permissions.canMove = true;
            InputEventHandler.instance.PlayerMoved -= OnPlayerMoved;
        }
    }


}
