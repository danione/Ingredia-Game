using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class TutorialStage : ScriptableObject
{
    public string title;
    public string description;
    public abstract void InitiateStage();
    public abstract void NextStage();
}
