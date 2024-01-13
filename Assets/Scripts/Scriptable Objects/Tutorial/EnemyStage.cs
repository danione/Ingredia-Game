using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Tutorial Stage", menuName = "Scriptable Objects/Tutorials/Enemy Stage")]
public class EnemyStage : TutorialStage
{
    public override void InitiateStage()
    {
        TutorialManager.instance.SpawnBat();
        GameEventHandler.Instance.DestroyedEnemy += TutorialManager.instance.OnEnemyDestroyed;
    }

    public override void NextStage()
    {
        GameEventHandler.Instance.DestroyedEnemy -= TutorialManager.instance.OnEnemyDestroyed;

    }
}
