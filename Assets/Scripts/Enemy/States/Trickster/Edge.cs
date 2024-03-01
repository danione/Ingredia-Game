using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private GameObject pointA;
    private GameObject pointB;

    public void Init(GameObject pointA, GameObject pointB)
    {
        this.pointA = pointA; this.pointB = pointB;

        GameEventHandler.Instance.PointDestroyed += CheckPoints;
        GameEventHandler.Instance.PointRevived += CheckIfActive;
        GameEventHandler.Instance.DestroyedObject += OnDestroyed;
    }

    private void OnDestroy()
    {
        try
        {
            GameEventHandler.Instance.PointDestroyed -= CheckPoints;
            GameEventHandler.Instance.PointRevived -= CheckIfActive;
            GameEventHandler.Instance.DestroyedObject -= OnDestroyed;
        }
        catch { }

    }

    private void OnDestroyed(GameObject obj)
    {
        if(obj == gameObject.transform.parent)
        {
            GameEventHandler.Instance.PointDestroyed -= CheckPoints;
            GameEventHandler.Instance.PointRevived -= CheckIfActive;
            GameEventHandler.Instance.DestroyedObject -= OnDestroyed;
        }
    }

    private void CheckPoints(GuardianPoint point)
    {
        if(pointA == point.gameObject || pointB == point.gameObject)
        {
            gameObject.SetActive(false);
        }
    }

    private void CheckIfActive(GuardianPoint point)
    {
        if(pointA == point.gameObject)
        {
            if (pointB.transform.GetChild(0).gameObject.activeSelf)
            {
                gameObject.SetActive(true);

            }
        } else if (pointB == point.gameObject) 
        { 
            if(pointA.transform.GetChild(0).gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SimpleProjectile projectile = other.gameObject.GetComponent<SimpleProjectile>();
        if (projectile != null && projectile.IsSourcePlayer())
        {
            GameEventHandler.Instance.DestroyObject(other.gameObject);
        }
    }
}
