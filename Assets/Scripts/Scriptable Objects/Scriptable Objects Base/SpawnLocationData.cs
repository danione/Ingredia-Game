using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Location", menuName = "Scriptable Objects/Spawn Data/Location")]
public class SpawnLocationData: ScriptableObject
{
    public float xLeftMax;
    public float xRightMax;
    public float yLocation;
}

