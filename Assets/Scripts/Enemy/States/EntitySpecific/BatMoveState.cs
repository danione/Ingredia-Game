using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMoveState : MoveState
{
    public BatMoveState(PlayerController controller, Enemy currentUnit, float movementSpeed) : base(controller, currentUnit, movementSpeed)
    {
    }

    public override void Move()
    {
        // Calculate the direction from the current position to the player's position
        float direction = controller.transform.position.x - currentUnit.gameObject.transform.position.x;

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
