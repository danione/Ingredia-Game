using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStopperEnemy : Enemy
{
    private Vector3 point;
    [SerializeField] private float radiusOfSuffocation;
    [SerializeField] private GameObject visualPoint;
    private bool areWeFreezing = true;
    private bool coroutineEnabled = false;

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
            if (areWeFreezing)
            {
                Freeze(colliders);
            } else if (!coroutineEnabled)
            {
                coroutineEnabled = true;
                StartCoroutine(Unfreeze(colliders));
            }
        }
    }

    private void Freeze(Collider[] colliders)
    {
        int countOfFrozenObjects = 0;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ingredient") || collider.gameObject.CompareTag("Projectile"))
            {
                collider.GetComponent<FallableObject>().SwapToSuck(point);
                countOfFrozenObjects++;
            }
        }

        if(countOfFrozenObjects > 5)
        {
            areWeFreezing = false;
        }
    }

    IEnumerator Unfreeze(Collider[] colliders)
    {
        yield return new WaitForSeconds(1);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ingredient") || collider.gameObject.CompareTag("Projectile"))
            {
                collider.GetComponent<FallableObject>().SwapToMove();
            }
        }
        yield return new WaitForSeconds(5);

        areWeFreezing = true;
        coroutineEnabled = false;
    }
}
