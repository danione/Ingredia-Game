using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleProjectile : FallableObject
{
    [SerializeField] protected List<string> target = new ();
    [SerializeField] private Vector3 direction;
    [SerializeField] protected int strength;
    private bool isSourcePlayer = false;

    public override void Move()
    {
        gameObject.transform.Translate(direction * Time.deltaTime * fallSpeed);
    }

    public void SetSource(bool asPlayer)
    {
        isSourcePlayer = asPlayer;
    }

    public bool IsSourcePlayer() {  return isSourcePlayer; }

    public override void SwapToFreeze()
    {
        base.SwapToFreeze();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null) { return; }

        HandleCollision(other);
    }

    public abstract void HandleCollision(Collider other);

    public void ChangeStrength(int newStrength)
    {
        strength = newStrength;
    }
}
