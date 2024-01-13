using System;
using UnityEngine;
using static UnityEngine.UI.Image;

[Serializable]
public class NonExistentLineRendererComponent : Exception
{
    public NonExistentLineRendererComponent(string message) : base(message) { }
}

[Serializable]
public class NonExistentLaserOrigin : Exception
{
    public NonExistentLaserOrigin(string message) : base(message) { }
}

public class LaserBeam
{
    private Transform attachedEntity;
    private LineRenderer laserLine;
    private Transform laserOrigin;

    private Vector3 direction;
    private Ray ray;
    private RaycastHit rayData;
    private float laserStrength = 0.3f;

    public LaserBeam(Transform entity, Vector3 direction)
    {
        attachedEntity = entity;
        this.direction = direction;
        laserLine = attachedEntity.GetComponentInChildren<LineRenderer>();
        if (laserLine == null)
        {
            throw new NonExistentLineRendererComponent("Laser Beams require line rendered component");
        }

        try
        {
            laserOrigin = laserLine.gameObject.transform.GetChild(0); // Origin Point
        }
        catch
        {
            throw new NonExistentLaserOrigin("Laser Beam does not have an origin!");
        }
    }

    public void Refresh()
    {
        laserLine.positionCount = 0;
    }


    public void Execute()
    {
        laserLine.positionCount = 2; // After refresh, we need to make sure we have the two ends of the laser
        ray = new Ray(attachedEntity.position, direction);
        laserLine.SetPosition(0, laserOrigin.position);

        if (Physics.Raycast(ray, out rayData))
        {
            Vector3 positionToRender = new Vector3(laserOrigin.position.x, rayData.point.y, attachedEntity.position.z);
            laserLine.SetPosition(1, rayData.point);
            Enemy enemy = rayData.collider.gameObject.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(laserStrength);
            }
            else if (rayData.collider.gameObject.CompareTag("Ingredient"))
            {
                UnityEngine.Object.Destroy(rayData.collider.gameObject);

            }
        }
        else
        {
            Vector3 positionToRender = new Vector3(laserOrigin.position.x, 50f, attachedEntity.position.z);
            laserLine.SetPosition(1, positionToRender);
        }
        
    }
}
