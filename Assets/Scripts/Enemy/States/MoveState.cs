using UnityEngine;
public abstract class MoveState : IState
{
    protected PlayerController controller;
    protected Vector3 playerLocation;
    protected Enemy currentUnit;
    protected float movementSpeed;
    protected float boundaryOfChange = 1.75f;

    public MoveState(PlayerController controller, Enemy currentUnit, float movementSpeed)
    {
        this.controller = controller;
        this.currentUnit = currentUnit;
        this.movementSpeed = movementSpeed;
    }
    public MoveState(PlayerController controller, Enemy currentUnit, float movementSpeed, float boundaryOfChange)
    {
        this.controller = controller;
        this.currentUnit = currentUnit;
        this.movementSpeed = movementSpeed;
        this.boundaryOfChange = boundaryOfChange;
    }

    public virtual void Enter()
    {
        if(controller != null)
        {
            playerLocation = controller.gameObject.transform.position;
        }
    }

    public void Exit()
    {
        // Debug.Log("Not required Exit");
    }

    public abstract void Move();

    void IState.Update()
    {
        Move();
    }
}
