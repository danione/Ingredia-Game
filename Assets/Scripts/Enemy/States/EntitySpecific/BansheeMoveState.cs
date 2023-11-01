using UnityEngine;

public class BansheeMoveState : MoveState
{
    private Boundaries m_Bounds;
    private Vector3 direction;
    private Vector3 m_targetPosition;
    private const float differenceTolerance = 0.3f;

    public BansheeMoveState(PlayerController controller, Enemy currentUnit, float movementSpeed, Boundaries boundaries) : base(controller, currentUnit, movementSpeed)
    {
        m_Bounds = boundaries;
    }

    public override void Enter()
    {
        base.Enter();
        float xPickerLeft = Mathf.Max(m_Bounds.xLeftBoundary, m_Bounds.xLeftBoundary);
        float xPickerRight = Mathf.Min(m_Bounds.xRightBoundary, m_Bounds.xRightBoundary);
        float yPickerTop = Mathf.Max(m_Bounds.yTopBoundary, m_Bounds.yTopBoundary);
        float yPickerBottom = Mathf.Min(m_Bounds.yBottomBoundary, m_Bounds.yBottomBoundary);
        float xPos = Random.Range(xPickerLeft, xPickerRight);
        float yPos = Random.Range(yPickerTop, yPickerBottom);

        

        m_targetPosition = new Vector3(xPos, yPos, 2);

        direction = m_targetPosition - currentUnit.transform.position;
        direction.Normalize();
        Debug.Log(m_targetPosition);
    }

    private bool HasReachedDestination()
    {
        return Mathf.Abs(m_targetPosition.x - currentUnit.transform.position.x) <= differenceTolerance
            && Mathf.Abs(m_targetPosition.y - currentUnit.transform.position.y) <= differenceTolerance;
    }

    public override void Move()
    {

        if (!HasReachedDestination())
        {
            currentUnit.transform.position += direction * movementSpeed * Time.deltaTime;
        }
        else
        {
            currentUnit.GetComponent<BansheeEnemy>().ReachedDestination();
        }

    }
}
