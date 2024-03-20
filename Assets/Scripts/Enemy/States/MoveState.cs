using UnityEngine;
public abstract class MoveState : IState
{
    protected PlayerController controller;
    protected Vector3 playerLocation;
    protected Enemy currentUnit;
    protected EnemyStateData enemyStateData;

    public MoveState(PlayerController controller, Enemy currentUnit, EnemyStateData data)
    {
        this.controller = controller;
        this.currentUnit = currentUnit;
        this.enemyStateData = data;
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
