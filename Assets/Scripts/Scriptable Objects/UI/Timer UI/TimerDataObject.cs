using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Timer Data", menuName = "Scriptable Objects/UI/Timers")]
public class TimerDataObject : ScriptableObject
{
    public string nameOfFeature;
    public Sprite BorderSpriteIdle;
    public Sprite BorderSpriteEngaged;
    public Sprite IconSpriteIdle;
    public Sprite IconSpriteEngaged;
    public string ButtonInfo;
}
