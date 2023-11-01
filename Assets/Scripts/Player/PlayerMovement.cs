using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] public float movementSpeed;
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;
    private bool isMovementEnabled = true;

    private void Start()
    {
        InputEventHandler.instance.MovementInputTampering += SetEnabledMovement;
    }

    public void Move()
    {
        if (!isMovementEnabled) { return; }

        float goingLeft = Input.GetAxis("Horizontal");

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
            transform.Translate(movementSpeed * goingLeft * Time.deltaTime * Vector3.left);
        }
    }

    public void SetMovementSpeed(float newMovementSpeed)
    {
        movementSpeed = newMovementSpeed;
    }

    public void SetEnabledMovement(bool newValue)
    {
        Debug.Log("Tuk sum");
        isMovementEnabled = newValue;
    }
}
