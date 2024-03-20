using System.Data;
using UnityEngine;

public class RevultedState : IState
{

    private UpgradedBatEnemy enemy;
    private Vector3 direction;
    private EnemyStateData enemyData;

    public RevultedState(Enemy enemy, EnemyStateData data) { 
        this.enemy = enemy.GetComponent<UpgradedBatEnemy>();
        enemyData = data;
    }


    public void Enter()
    {
        if(enemy == null) {
            throw new NoNullAllowedException("Upgraded Bats needed here");
        }
        direction = enemy.GetDirection();
    }

    public void Exit()
    {
        // Nothing
    }

    public void Update()
    {
        enemy.gameObject.transform.Translate((enemyData.MovementSpecs.movementSpeed / 2)  * Time.deltaTime * direction);
    }
}
