using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : PlayerUpgrade
{
    public override void Init()
    {
        _currentStage = 1;
        value = 2;
        _name = "Endurance Upgrade " + _currentStage;
        _controller = PlayerController.Instance;
        _description = "This upgrade will increase the player's health by " + value;
        _maxStage = 5;
        _goldCost = 10;
    }

    public override void Upgrade()
    {
        if (_isAvailable && PlayerController.Instance.inventory.gold - _goldCost > 0)
        {
            _controller.stats.UpgradeHealth(value);
            PlayerController.Instance.inventory.AddGold(-_goldCost);
            Debug.Log("Current health is now " + _controller.stats.Health);
            IterateStage();
        }
    }

    private void IterateStage()
    {
        if(_currentStage < _maxStage)
        {
            _currentStage++;
            value *= _currentStage;
            _goldCost *= _currentStage;
        }
        else
        {
            _isAvailable = false;
        }
    }
}
