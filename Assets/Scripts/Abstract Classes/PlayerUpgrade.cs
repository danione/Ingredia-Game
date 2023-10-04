using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerUpgrade : IUpgrade
{

    protected string _name;
    public string Name => _name;
    protected string _description;
    public string Description => _description;
    protected int _maxStage;
    public int MaxStage => _maxStage;
    protected int _currentStage;
    public int CurrentStage => _currentStage;
    protected bool _isAvailable = true;
    public bool IsAvailable => _isAvailable;
    protected int _goldCost = 0;
    public int GoldCost => _goldCost;

    protected int value;

    protected PlayerController _controller = null;

    public abstract void Upgrade();

    public abstract void Init();
}
