using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pulsing Data", menuName = "Scriptable Objects/UI/Pulsing Data")]
public class TimerPulsingObject : ScriptableObject
{
    public Vector3 minScale;
    public Vector3 maxScale;
    public float scalingSpeed;
    public float scalingDurationStart;
    public float scalinDurationFirstCheckpoint;
    public float scalingDurtationLastCheckpoint;
}
