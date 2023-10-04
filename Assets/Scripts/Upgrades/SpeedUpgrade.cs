using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : PlayerUpgrade
{
    public override void Init()
    {
        _currentStage = 1;
        value = 20;
        _name = "Swiftness Upgrade " + _currentStage;
        _controller = PlayerController.Instance;
        _description = "This upgrade will increase the player's health by " + value;
        _maxStage = 3;
        _goldCost = 40;
    }

    public override void Upgrade()
    {
        if (_isAvailable && PlayerController.Instance.inventory.gold - _goldCost > 0)
        {
            PlayerMovement movement = PlayerController.Instance.GetComponent<PlayerMovement>();
            if(movement != null)
            {
                movement.SetMovementSpeed(value);
                PlayerController.Instance.inventory.AddGold(-_goldCost);
                Debug.Log("Current movement is now " + movement.movementSpeed);
                IterateStage();
            }
        }
    }

    private void IterateStage()
    {
        if (_currentStage < _maxStage)
        {
            _currentStage++;
            value += 10;
            _goldCost *= _currentStage;
        }
        else
        {
            _isAvailable = false;
        }
    }


}
