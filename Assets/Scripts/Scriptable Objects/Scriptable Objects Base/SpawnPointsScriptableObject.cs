using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Location", menuName = "Scriptable Objects/Spawn Location")]
public class SpawnPointsScriptableObject : ScriptableObject
{
    public float xLeftMax;
    public float xRightMax;
    public float yLocation;
}
