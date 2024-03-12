using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;
    [SerializeField] private float movementSpeedModifier = 0f;
    private float modifierCount = 0f;
    private float moveDirectionExternalControl = 0f;

    private void Start()
    {
        InputEventHandler.instance.MoveRandomly += OnMoveRandomly;
        InputEventHandler.instance.PickDirection += OnPickRandomDirection;
        InputEventHandler.instance.MoveTowardsTarget += OnMoveTowards;
        PlayerEventHandler.Instance.MovementSpeedAdjusted += OnMoveSpeedAdjusted;
    }

    public void Move()
    {
        float goingLeft = Input.GetAxis("Horizontal");

        if(goingLeft != 0f) { InputEventHandler.instance.PlayerMoving(goingLeft); }

        if (!PlayerInputHandler.permissions.canMove) return;

        MovementValidation(goingLeft);
    }

    private void OnMoveSpeedAdjusted(bool modifier)
    {
        if (modifier) modifierCount++;
        else {
            modifierCount--;
        }
    }

    private void MovementValidation(float direction)
    {
        if (transform.position.x > leftBorder)
        {
            transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < rightBorder)
        {
            transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
        }
        else
        {
            transform.Translate((movementSpeed - (movementSpeedModifier * modifierCount)) * direction * Time.deltaTime * Vector3.left);
        }
    }

    public void SetMovementSpeed(float newMovementSpeed)
    {
        movementSpeed = newMovementSpeed;
    }

    public void OnMoveRandomly()
    {
        MovementValidation(moveDirectionExternalControl);
    }

    public void OnMoveTowards(float direction)
    {
        MovementValidation(direction);
    }
    public void OnPickRandomDirection()
    {
        if(Random.value < 0.5f) moveDirectionExternalControl = -1f;
        else moveDirectionExternalControl = 1f;
    }
}
