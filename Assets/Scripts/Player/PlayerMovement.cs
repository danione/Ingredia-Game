using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;

    public void Move()
    {
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
}
