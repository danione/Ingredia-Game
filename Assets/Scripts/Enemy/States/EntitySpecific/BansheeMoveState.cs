using UnityEngine;

public class BansheeMoveState : MoveState
{
    private BoundariesData m_Bounds;
    private Vector3 direction;
    private Vector3 m_targetPosition;
    private const float differenceTolerance = 0.3f;

    public BansheeMoveState(PlayerController controller, Enemy currentUnit, EnemyStateData stateData, BoundariesData boundaries) : base(controller, currentUnit, stateData)
    {
        m_Bounds = boundaries;
    }

    public override void Enter()
    {
        base.Enter();
        float xPos = Random.Range(m_Bounds.xLeftMax, m_Bounds.xRightMax);
        float yPos = Random.Range(m_Bounds.yTopMax, m_Bounds.yBottomMax);

        

        m_targetPosition = new Vector3(xPos, yPos, 2);

        direction = m_targetPosition - currentUnit.transform.position;
        direction.Normalize();
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
            currentUnit.transform.position += direction * enemyStateData.MovementSpecs.movementSpeed * Time.deltaTime;
        }
        else
        {
            currentUnit.GetComponent<BansheeEnemy>().ReachedDestination();
        }

    }
}
