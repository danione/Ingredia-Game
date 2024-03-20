using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy State", menuName = "Scriptable Objects/Enemy/Enemy State")]
public class EnemyStateData : ScriptableObject
{
    public MovementStateSpecifics MovementSpecs;
}

[System.Serializable]
public class MovementStateSpecifics
{
    public float movementSpeed;
    public float trackingCooldown;
    public float movementBoundaries;
}
