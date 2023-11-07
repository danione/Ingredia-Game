using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : FallableObject
{
    [SerializeField] private List<string> target = new ();
    [SerializeField] private Vector3 direction;
    [SerializeField] private int strength;

    public override void Move()
    {
        gameObject.transform.Translate(direction * Time.deltaTime * fallSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(target == null) { return; }

        IUnitStats unitStats = other.GetComponent<IUnitStats>();
        bool isValidTarget = target.Contains(other.tag);

        if (isValidTarget && unitStats != null)
        {
            unitStats.TakeDamage(strength);
            Destroy(gameObject);
        } else if (isValidTarget)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
