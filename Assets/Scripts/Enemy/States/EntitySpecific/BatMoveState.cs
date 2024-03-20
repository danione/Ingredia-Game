using System.Data;
using UnityEngine;

public class BatMoveState : MoveState
{
    private float currentPositionDifferenceX;
    private float trackingCooldown;
    private bool isTrackingPlayer = false;
    private BatEnemy _batEnemy;

    public BatMoveState(PlayerController controller, Enemy currentUnit, EnemyStateData stateData) : base(controller, currentUnit, stateData)
    {
        _batEnemy = currentUnit.GetComponent<BatEnemy>();

        if (_batEnemy == null)
            throw new NoNullAllowedException("Bat enemy cannot be null");
    }

    public override void Enter()
    {
        base.Enter();
        isTrackingPlayer = true;
        trackingCooldown = 0;
    }

    private void FollowPlayerAndShoot()
    {
        currentPositionDifferenceX = GetCurrentPositionDifferenceX();
        if (currentPositionDifferenceX < enemyStateData.MovementSpecs.movementBoundaries)
        {
            isTrackingPlayer = false;
            trackingCooldown = enemyStateData.MovementSpecs.trackingCooldown;
            _batEnemy.Shoot();
        }
        else if (currentPositionDifferenceX >= enemyStateData.MovementSpecs.movementBoundaries && isTrackingPlayer)
        {
            MoveLocation();
        }
    }

    private float GetCurrentPositionDifferenceX()
    {
        return Mathf.Abs(PlayerController.Instance.gameObject.transform.position.x - currentUnit.transform.position.x);
    }

    public override void Move()
    {
        if(isTrackingPlayer)
        {
            FollowPlayerAndShoot();
        }
        else
        {
            trackingCooldown -= Time.deltaTime;
            if (trackingCooldown < 0)
            {
                isTrackingPlayer = true;
            }
        }
    }

    private void MoveLocation()
    {
        Vector3 direction = (controller.transform.position - currentUnit.gameObject.transform.position).normalized;

        // Only consider the x component for movement
        direction = new Vector3(direction.x, 0f, 0f);

        // Calculate the new position by interpolating between the current position
        // and the player's position using movementSpeed and Time.deltaTime
        currentUnit.transform.Translate(enemyStateData.MovementSpecs.movementSpeed * Time.deltaTime * direction);
    }
}
