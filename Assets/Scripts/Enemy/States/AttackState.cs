using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : IState
{
    protected PlayerController controller;
    protected Enemy currentUnit;

    public AttackState(PlayerController controller, Enemy currentUnit)
    {
       this.controller = controller;
        this.currentUnit = currentUnit;
    }

    public virtual void Enter()
    {
        // Enter
    }

    public virtual void Exit()
    {
        // Exit
    }

    public abstract void Attack();

    void IState.Update()
    {
        Attack();
    }
}
