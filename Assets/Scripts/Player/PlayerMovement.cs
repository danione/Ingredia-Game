using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] public float movementSpeed;
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;
    private float moveDirectionExternalControl = 0f;
    private bool isMovementEnabled = true;

    private void Start()
    {
        InputEventHandler.instance.MovementInputTampering += SetEnabledMovement;
        InputEventHandler.instance.MoveRandomly += OnMoveRandomly;
        InputEventHandler.instance.PickDirection += OnPickRandomDirection;
        InputEventHandler.instance.MoveTowardsTarget += OnMoveTowards;
    }

    public void Move()
    {
        if (!isMovementEnabled) return;

        float goingLeft = Input.GetAxis("Horizontal");

        MovementValidation(goingLeft);
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
            transform.Translate(movementSpeed * direction * Time.deltaTime * Vector3.left);
        }
    }

    public void SetMovementSpeed(float newMovementSpeed)
    {
        movementSpeed = newMovementSpeed;
    }

    public void SetEnabledMovement(bool newValue)
    {
        isMovementEnabled = newValue;
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
