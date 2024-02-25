using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterProjectile : SimpleProjectile
{
    private Transform followTarget;
    [SerializeField] private float speedOfFollow;
    [SerializeField] private float stopFollowing;
    [SerializeField] private float stepPerAngle;
    [SerializeField] private float decreaser;
    private Vector3 initialDirection;

    private void Start()
    {
        followTarget = PlayerController.Instance.transform;
    }

    public override void Move()
    {

        if (transform.position.y > stopFollowing)
        {
            // Calculate the direction towards the target
            initialDirection = (followTarget.position - transform.position).normalized;

            // Calculate the angle towards the target
            float angle = Mathf.Atan2(initialDirection.x, -initialDirection.y) * Mathf.Rad2Deg;

            // Rotate towards the target
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        transform.position += initialDirection * speedOfFollow * Time.deltaTime;
    }


    public override void HandleCollision(Collider other)
    {
        if (target.Contains(other.tag))
        {
            IUnitStats stats = other.GetComponent<IUnitStats>();
            stats?.TakeDamage(strength);
            GameEventHandler.Instance.DestroyObject(gameObject);
        }
    }
}
