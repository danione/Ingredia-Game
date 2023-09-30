using UnityEngine;

public class SimpleProjectile : FallableObject
{
    [SerializeField] private string target = "Player";
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

        if(other.CompareTag(target) && unitStats != null)
        {
            unitStats.TakeDamage(strength);
            Destroy(gameObject);
        }

        
    }
}
