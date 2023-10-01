using UnityEngine;
public class MoveState : IState
{
    private PlayerController controller;
    private Vector3 playerLocation;
    private Enemy currentUnit;
    private float movementSpeed;
    private float boundaryOfChange = 1.75f;

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

    public void Enter()
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


    void IState.Update()
    {
        
        // Calculate the direction from the current position to the player's position
        float direction = controller.gameObject.transform.position.x - currentUnit.gameObject.transform.position.x;
        
        // Calculate the new position by interpolating between the current position
        // and the player's position using movementSpeed and Time.deltaTime
        float newXPosition = currentUnit.gameObject.transform.position.x + direction * movementSpeed * Time.deltaTime;

        currentUnit.gameObject.transform.position = new Vector3(
            newXPosition,
            currentUnit.gameObject.transform.position.y,
            currentUnit.gameObject.transform.position.z
        ); 
    }
}
