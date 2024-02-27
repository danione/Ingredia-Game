using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuffocatorEnemy : Enemy
{
    private Vector3 point;
    [SerializeField] private float radiusOfSuffocation;
    [SerializeField] private GameObject visualPoint;

    private void Start()
    {
        point = new Vector3(Random.Range(enemyData.spawnBoundaries.xLeftMax, enemyData.spawnBoundaries.xRightMax), 
            Random.Range(enemyData.spawnBoundaries.yBottomMax, enemyData.spawnBoundaries.yTopMax), 2);
        visualPoint.transform.position = point;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(point, radiusOfSuffocation);
        if(colliders.Length > 0 )
        {
            foreach(Collider collider in colliders)
            {
                if(collider.gameObject.CompareTag("Ingredient") || collider.gameObject.CompareTag("Projectile"))
                {
                    GameEventHandler.Instance.DestroyObject(collider.gameObject);
                }
            }
        }
    }

}
