using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boundaries Data", menuName = "Scriptable Objects/Spawn Data/Boundaries")]
public class BoundariesData : ScriptableObject
{
    public float xLeftMax;
    public float xRightMax;
    public float yTopMax;
    public float yBottomMax;
    public float offsetX;
    public float offsetY;
}
