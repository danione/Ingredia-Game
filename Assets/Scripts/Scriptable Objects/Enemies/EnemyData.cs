using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Objects/Enemy/Enemy")]
public class EnemyData : ScriptableObject
{
    public int health;
    public float movementSpeed;
    public int goldDrop;
    public BoundariesData spawnBoundaries;
}
