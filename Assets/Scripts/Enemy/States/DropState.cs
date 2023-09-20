using UnityEngine;

public class DropState : IState
{
    private GameObject objectToDrop;
    private float frequency;

    public DropState(GameObject objectToDrop, float frequency)
    {
        this.objectToDrop = objectToDrop;
        this.frequency = frequency;
    }

    public void Enter()
    {
        Debug.Log("No Enter Required");
    }

    public void Exit()
    {
        Debug.Log("No Exit Required");
    }

    public void Update()
    {
        
    }
}
