using UnityEngine;

public abstract class TutorialStage : ScriptableObject
{
    public string title;
    [TextArea(4, 10)] 
    public string description;
    public abstract void InitiateStage();
    public abstract void NextStage();
}
